import {
  ProductBuildOption,
  ProductCategoryBuildOption,
  UserAddressBuildOption,
  UserBuildOption,
} from "./src/constant";
import { writeCsvFile } from "./src/helper";

const FOLDER_PATH = "data";

async function main() {
  const productCategories = await writeCsvFile({
    filePath: FOLDER_PATH,
    ...ProductCategoryBuildOption,
  });

  await writeCsvFile({
    filePath: FOLDER_PATH,
    ...ProductBuildOption,
    schemaBuilder: () =>
      ProductBuildOption.schemaBuilder({
        categoryIds: productCategories.map((item) => item.id),
      }),
  });
  
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
}

(async function () {
  await main();
})();
