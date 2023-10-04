import { faker } from "@faker-js/faker";
import { readCsvFile, writeCsvFile } from "./src/helper";
import { productSchemaBuilder } from "./src/schema-builder";

// writeCsvFile({
//   fileName: "test",
//   count: 10,
//   schemaBuilder: productSchemaBuilder,
// });
const res = (async function () {
  let sha1sum = await readCsvFile({
    fileName: "test",
    schemaBuilder: productSchemaBuilder,
  });
  console.log(sha1sum);
})();

console.log("🚀 ~ file: index.ts:15 ~ res:", res);
