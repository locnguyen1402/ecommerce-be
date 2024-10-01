START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001041649_UpdateProductSkuAndCode') THEN
    ALTER TABLE products ALTER COLUMN sku TYPE character varying(100);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001041649_UpdateProductSkuAndCode') THEN
    ALTER TABLE products ADD code character varying(50) NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001041649_UpdateProductSkuAndCode') THEN
    ALTER TABLE product_variants ADD code character varying(50) NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001041649_UpdateProductSkuAndCode') THEN
    ALTER TABLE product_variants ADD sku character varying(100) NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001041649_UpdateProductSkuAndCode') THEN
    ALTER TABLE product_variants ADD CONSTRAINT "CK_ProductVariant_Stock" CHECK ("stock" >= 0);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001041649_UpdateProductSkuAndCode') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20241001041649_UpdateProductSkuAndCode', '8.0.1');
    END IF;
END $EF$;
COMMIT;

