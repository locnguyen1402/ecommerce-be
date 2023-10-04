import { pipeline } from "node:stream/promises";
import csv from "csv-parser";
import fs from "fs";

export type WriteCsvFileOptions = {
  filePath?: string;
  fileName: string;
  schemaBuilder: () => Record<string, any>;
  count: number;
};

export const writeCsvFile = (options: WriteCsvFileOptions) => {
  const baseData = options.schemaBuilder();
  const headers = Object.keys(baseData);

  let stream = fs.createWriteStream(`${options.fileName}.csv`);
  stream.write(headers.toString() + "\n");

  for (let i = 0; i < options.count; i++) {
    const data = options.schemaBuilder();

    const rowData: any[] = [];
    headers.forEach((key) => {
      rowData.push(data[key]);
    });

    stream.write(rowData.toString() + "\n");
  }

  stream.end();
};

export type ReadCsvFileOptions<T extends Record<string, any>> = {
  filePath?: string;
  fileName: string;
  schemaBuilder: () => T;
};

export const readCsvFile = async <TResult extends Record<string, any>>(
  options: ReadCsvFileOptions<TResult>
) => {
  const baseData = options.schemaBuilder();
  const headers = Object.keys(baseData);

  const reader = fs.createReadStream(`${options.fileName}.csv`);
  const result: any[] = [];
  const results = [];

  reader
    .pipe(csv({ headers }))
    .on("data", (data) => results.push(data))
    .on("end", () => {
      console.log(results);
      // [
      //   { NAME: 'Daffy Duck', AGE: '24' },
      //   { NAME: 'Bugs Bunny', AGE: '22' }
      // ]
    });

  return pipeline(reader)
};
