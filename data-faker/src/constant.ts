import {
  BuildOption,
  Product,
  ProductCategory,
  User,
  UserAddress,
} from "./models";
import {
  productSchemaBuilder,
  productCategorySchemaBuilder,
  userSchemaBuilder,
  userAddressSchemaBuilder,
} from "./schema-builder";

export const ProductBuildOption: BuildOption<Product> = {
  fileName: "products",
  count: 100000,
  schemaBuilder: productSchemaBuilder,
};

export const ProductCategoryBuildOption: BuildOption<ProductCategory> = {
  fileName: "product_categories",
  count: 10,
  schemaBuilder: productCategorySchemaBuilder,
};

export const UserBuildOption: BuildOption<User> = {
  fileName: "users",
  count: 10,
  schemaBuilder: userSchemaBuilder,
};

export const UserAddressBuildOption: BuildOption<UserAddress> = {
  fileName: "user_addresses",
  count: 10,
  schemaBuilder: userAddressSchemaBuilder,
};
