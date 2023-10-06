import {
  productCategorySchemaBuilder,
  productSchemaBuilder,
  userAddressSchemaBuilder,
  userSchemaBuilder,
} from "./schema-builder";

export type SchemaBuilderFunc<TSchema extends Record<string, any>> = (
  ...args: any[]
) => TSchema;

export type BuildOption<TSchema extends Record<string, any>> = {
  fileName: string;
  count: number;
  schemaBuilder: SchemaBuilderFunc<TSchema>;
};

export type Product = ReturnType<typeof productSchemaBuilder>;
export type ProductCategory = ReturnType<typeof productCategorySchemaBuilder>;

export type User = ReturnType<typeof userSchemaBuilder>;
export type UserAddress = ReturnType<typeof userAddressSchemaBuilder>;
