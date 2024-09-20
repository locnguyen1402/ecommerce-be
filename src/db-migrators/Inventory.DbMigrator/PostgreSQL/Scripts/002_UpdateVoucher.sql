START TRANSACTION;


DO $EF$
BEGIN
    IF EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920155521_UpdateVoucher') THEN
    ALTER TABLE vouchers DROP CONSTRAINT fk_vouchers_merchants_merchant_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920155521_UpdateVoucher') THEN
    DROP INDEX ix_vouchers_merchant_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920155521_UpdateVoucher') THEN
    ALTER TABLE vouchers DROP COLUMN merchant_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920155521_UpdateVoucher') THEN
    ALTER TABLE vouchers DROP COLUMN status;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920155521_UpdateVoucher') THEN
    DELETE FROM "__EFMigrationsHistory"
    WHERE migration_id = '20240920155521_UpdateVoucher';
    END IF;
END $EF$;
COMMIT;

