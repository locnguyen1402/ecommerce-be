START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240907105133_UpdateProductVariantAttributeValue') THEN
    ALTER TABLE product_variant_attribute_values ADD attribute_value_id uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240907105133_UpdateProductVariantAttributeValue') THEN
    ALTER TABLE product_attributes ADD is_active boolean NOT NULL DEFAULT FALSE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240907105133_UpdateProductVariantAttributeValue') THEN
    ALTER TABLE product_attributes ADD predefined boolean NOT NULL DEFAULT FALSE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240907105133_UpdateProductVariantAttributeValue') THEN
    CREATE TABLE attribute_values (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        value character varying(200) NOT NULL,
        product_attribute_id uuid,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_attribute_values PRIMARY KEY (id),
        CONSTRAINT fk_attribute_values_product_attributes_product_attribute_id FOREIGN KEY (product_attribute_id) REFERENCES product_attributes (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240907105133_UpdateProductVariantAttributeValue') THEN
    CREATE INDEX ix_product_variant_attribute_values_attribute_value_id ON product_variant_attribute_values (attribute_value_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240907105133_UpdateProductVariantAttributeValue') THEN
    CREATE INDEX ix_attribute_values_product_attribute_id ON attribute_values (product_attribute_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240907105133_UpdateProductVariantAttributeValue') THEN
    ALTER TABLE product_variant_attribute_values ADD CONSTRAINT fk_product_variant_attribute_values_attribute_values_attribute FOREIGN KEY (attribute_value_id) REFERENCES attribute_values (id) ON DELETE CASCADE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240907105133_UpdateProductVariantAttributeValue') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240907105133_UpdateProductVariantAttributeValue', '8.0.1');
    END IF;
END $EF$;
COMMIT;

