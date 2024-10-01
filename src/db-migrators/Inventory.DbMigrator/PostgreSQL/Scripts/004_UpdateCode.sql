START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001062501_UpdateCode') THEN
    CREATE UNIQUE INDEX ix_products_code ON products (code);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001062501_UpdateCode') THEN
    CREATE UNIQUE INDEX ix_product_variants_code ON product_variants (code);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20241001062501_UpdateCode') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20241001062501_UpdateCode', '8.0.1');
    END IF;
END $EF$;
COMMIT;

