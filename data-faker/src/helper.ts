import csv from "csv-parser";
import fs from "fs";
import { pipeline } from "stream";
import { promisify } from "util";

export const pipelineAsync = promisify(pipeline);

export type WriteCsvFileOptions<TSchema extends Record<string, any>> = {
  filePath?: string;
  fileName: string;
  schemaBuilder: () => TSchema;
  count: number;

  doneCb?: (insertedList: TSchema[]) => void;
};

export const writeCsvFile = <TSchema extends Record<string, any>>(
  options: WriteCsvFileOptions<TSchema>
) => {
  const baseData = options.schemaBuilder();
  const headers = Object.keys(baseData);

  let stream = fs.createWriteStream(`${options.filePath}/${options.fileName}.csv`);
  stream.write(headers.toString() + "\n");

  return new Promise<TSchema[]>((resolve, reject) => {
    const insertedList: TSchema[] = [];
    for (let i = 0; i < options.count; i++) {
      const data = options.schemaBuilder();

      const rowData: TSchema[] = [];
      headers.forEach((key) => {
        rowData.push(data[key]);
      });
      insertedList.push(data);

      stream.write(rowData.toString() + "\n");
    }

    stream.end(options.doneCb);
    resolve(insertedList);
  });
};

export type ReadCsvFileOptions<T extends Record<string, any>> = {
  filePath?: string;
  fileName: string;
  schemaBuilder: () => T;

  doneCb?: (data: T[]) => void;
};

export const readCsvFile = async <TResult extends Record<string, any>>(
  options: ReadCsvFileOptions<TResult>
) => {
  const baseData = options.schemaBuilder();
  const headers = Object.keys(baseData);

  const reader = fs.createReadStream(`${options.filePath}/${options.fileName}.csv`);

  return new Promise<TResult[]>((resolve, reject) => {
    const results: TResult[] = [];
    reader
      .pipe(csv({ headers }))
      .on("data", (data) => results.push(data))
      .on("end", () => {
        if (typeof options.doneCb === "function") {
          options.doneCb(results);
        }
        resolve(results);
      });
  });
};
