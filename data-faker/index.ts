import {
  ProductBuildOption,
  ProductCategoryBuildOption,
  ProductTagBuildOption,
  UserAddressBuildOption,
  UserBuildOption,
} from "./src/constant";
import { readCsvFile, writeCsvFile } from "./src/helper";

const FOLDER_PATH = "data";

async function main() {
  // const productCategories = await writeCsvFile({
  //   filePath: FOLDER_PATH,
  //   ...ProductCategoryBuildOption,
  // });

  // const productTags = await writeCsvFile({
  //   filePath: FOLDER_PATH,
  //   ...ProductTagBuildOption,
  // });

  // await writeCsvFile({
  //   filePath: FOLDER_PATH,
  //   ...ProductBuildOption,
  //   schemaBuilder: () =>
  //     ProductBuildOption.schemaBuilder({
  //       categoryIds: productCategories.map((item) => item.id),
  //     }),
  // });

  // const users = await writeCsvFile({
  //   filePath: FOLDER_PATH,
  //   ...UserBuildOption,
  // });

  // await writeCsvFile({
  //   filePath: FOLDER_PATH,
  //   ...UserAddressBuildOption,
  //   schemaBuilder: () =>
  //     UserAddressBuildOption.schemaBuilder({
  //       userIds: users.map((item) => item.id),
  //     }),
  // });

  const result = await readCsvFile({
    filePath: FOLDER_PATH,
    ...ProductBuildOption,
    schemaBuilder: () =>
      ProductBuildOption.schemaBuilder({
        categoryIds: ["sample"],
      }),
  });
  console.log(
    "🚀 ~ file: index.ts:50 ~ main ~ result:",
    result.filter((item) => !item.categoryId)
  );
}

(async function () {
  await main();
})();
