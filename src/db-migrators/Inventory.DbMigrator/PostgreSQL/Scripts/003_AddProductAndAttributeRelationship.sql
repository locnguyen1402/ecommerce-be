START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530161917_AddProductAndAttributeRelationship') THEN
    CREATE TABLE product_product_attributes (
        product_id uuid NOT NULL,
        product_attribute_id uuid NOT NULL,
        CONSTRAINT pk_product_product_attributes PRIMARY KEY (product_id, product_attribute_id),
        CONSTRAINT fk_product_product_attributes_product_attributes_product_attri FOREIGN KEY (product_attribute_id) REFERENCES product_attributes (id) ON DELETE CASCADE,
        CONSTRAINT fk_product_product_attributes_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530161917_AddProductAndAttributeRelationship') THEN
    CREATE INDEX ix_product_product_attributes_product_attribute_id ON product_product_attributes (product_attribute_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530161917_AddProductAndAttributeRelationship') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240530161917_AddProductAndAttributeRelationship', '8.0.1');
    END IF;
END $EF$;
COMMIT;

