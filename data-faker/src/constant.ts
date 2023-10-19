import {
  BuildOption,
  Product,
  ProductCategory,
  ProductTag,
  User,
  UserAddress,
} from "./models";
import {
  productSchemaBuilder,
  productCategorySchemaBuilder,
  productTagSchemaBuilder,
  userSchemaBuilder,
  userAddressSchemaBuilder,
} from "./schema-builder";

export const ProductBuildOption: BuildOption<Product> = {
  fileName: "products",
  count: 20000,
  schemaBuilder: productSchemaBuilder,
};

export const ProductCategoryBuildOption: BuildOption<ProductCategory> = {
  fileName: "product_categories",
  count: 10,
  schemaBuilder: productCategorySchemaBuilder,
};

export const ProductTagBuildOption: BuildOption<ProductTag> = {
  fileName: "product_tags",
  count: 100,
  schemaBuilder: productTagSchemaBuilder,
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
