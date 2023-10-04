import { faker } from "@faker-js/faker";

export const productTags = faker.helpers.multiple(
  faker.commerce.productAdjective,
  {
    count: 20,
  }
);

export const productSchemaBuilder = () => {
  return {
    id: faker.string.uuid(),
    name: faker.commerce.productName(),
    description: faker.commerce.productDescription(),
    price: faker.commerce.price({
      min: 0,
      max: 100000,
      dec: 0,
    }),
    tags: faker.helpers
      .arrayElements(productTags, { min: 0, max: 10 })
      .join("/"),
    categoryId: "string",
  };
};

export const productCategorySchemaBuilder = () => {
  return {
    id: faker.string.uuid(),
    name: faker.lorem.words({ min: 1, max: 3 }),
    description: faker.lorem.paragraph({ min: 1, max: 3 }),
  };
};
