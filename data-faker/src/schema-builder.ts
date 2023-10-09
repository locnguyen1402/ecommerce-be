import { faker } from "@faker-js/faker";

export const productTags = faker.helpers.multiple(
  faker.commerce.productAdjective,
  {
    count: 20,
  }
);

export const productSchemaBuilder = (config: { categoryIds: string[] }) => {
  return {
    id: faker.string.uuid(),
    name: faker.commerce.productName(),
    description: faker.lorem.sentences({ min: 1, max: 5 }),
    price: faker.commerce.price({
      min: 0,
      max: 100000,
      dec: 0,
    }),
    tags: faker.helpers
      .arrayElements(productTags, { min: 0, max: 10 })
      .join("/"),
    categoryId: faker.helpers.arrayElement(config.categoryIds),
  };
};

export const productCategorySchemaBuilder = () => {
  return {
    id: faker.string.uuid(),
    name: faker.lorem.words({ min: 1, max: 3 }),
    description: faker.lorem.paragraph({ min: 1, max: 3 }),
  };
};

export const userSchemaBuilder = () => {
  const gender = faker.person.sexType();
  const firstName = faker.person.firstName(gender);
  const lastName = faker.person.lastName(gender);

  return {
    id: faker.string.uuid(),
    userName: faker.internet.userName({
      firstName,
      lastName,
    }),
    gender,
    firstName,
    lastName,
    phoneNumber: faker.helpers.replaceSymbolWithNumber("09########"),
  };
};

export const userAddressSchemaBuilder = (config: { userIds: string[] }) => {
  const gender = faker.person.sexType();
  const firstName = faker.person.firstName(gender);
  const lastName = faker.person.lastName(gender);
  const fullName = faker.person.fullName({
    firstName,
    lastName,
    sex: gender,
  });

  return {
    id: faker.string.uuid(),
    userId: faker.helpers.arrayElement(config.userIds),
    fullName,
    phoneNumber: faker.helpers.replaceSymbolWithNumber("09########"),
    city: faker.location.city(),
    district: faker.lorem.words({ min: 1, max: 3 }),
    ward: faker.lorem.words({ min: 1, max: 3 }),
    addressLine1: faker.location.streetAddress(),
    addressLine2: faker.helpers.maybe(() => faker.location.streetAddress(), {
      probability: 0.5,
    }),
  };
};
