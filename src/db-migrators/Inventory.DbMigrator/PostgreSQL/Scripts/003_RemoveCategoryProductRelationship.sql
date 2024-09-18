START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918092543_RemoveCategoryProductRelationship') THEN
    DROP TABLE category_products;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918092543_RemoveCategoryProductRelationship') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240918092543_RemoveCategoryProductRelationship', '8.0.1');
    END IF;
END $EF$;
COMMIT;

