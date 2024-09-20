CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE categories (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(100) NOT NULL,
        slug character varying(150) NOT NULL,
        description character varying(500) NOT NULL DEFAULT (''),
        parent_id uuid,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        has_discounts_applied boolean NOT NULL,
        CONSTRAINT pk_categories PRIMARY KEY (id),
        CONSTRAINT fk_categories_categories_parent_id FOREIGN KEY (parent_id) REFERENCES categories (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE customers (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        user_name character varying(200),
        first_name character varying(200) NOT NULL,
        last_name character varying(200),
        full_name character varying(500) NOT NULL,
        birth_date date,
        gender character varying(50) DEFAULT ('UNSPECIFIED'),
        email character varying(100),
        phone_number character varying(20),
        ref_user_id text,
        level_type character varying(50) NOT NULL DEFAULT ('SILVER'),
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_customers PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE discounts (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(150) NOT NULL,
        code character varying(100) NOT NULL,
        description character varying(500) DEFAULT (''),
        discount_type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        discount_value numeric,
        discount_unit character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        min_order_value numeric,
        max_discount_amount numeric,
        start_date timestamp with time zone,
        end_date timestamp with time zone,
        is_active boolean NOT NULL DEFAULT TRUE,
        limitation_times integer,
        limitation_type character varying(50) DEFAULT ('UNSPECIFIED'),
        discount_usage_history jsonb NOT NULL DEFAULT ('[]'),
        discount_id uuid,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        has_discounts_applied boolean NOT NULL,
        CONSTRAINT pk_discounts PRIMARY KEY (id),
        CONSTRAINT fk_discounts_discounts_discount_id FOREIGN KEY (discount_id) REFERENCES discounts (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE merchants (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(150) NOT NULL,
        merchant_number text,
        description character varying(500) DEFAULT (''),
        is_active boolean NOT NULL DEFAULT TRUE,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_merchants PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE product_attributes (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(100) NOT NULL,
        predefined boolean NOT NULL,
        is_active boolean NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_product_attributes PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE vouchers (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        code character varying(150) NOT NULL,
        start_date timestamp with time zone NOT NULL,
        end_date timestamp with time zone NOT NULL,
        applied_on_type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        target_customer_type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        popular_type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        min_spend numeric(19,2) NOT NULL DEFAULT 0.0,
        max_quantity integer NOT NULL DEFAULT 0,
        max_quantity_per_user integer NOT NULL DEFAULT 1,
        type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        discount_type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        value numeric(19,2) NOT NULL DEFAULT 0.0,
        max_value numeric(19,2),
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_vouchers PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE contacts (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        type integer NOT NULL,
        name character varying(200),
        contact_name character varying(200),
        phone_number character varying(20),
        address_info jsonb NOT NULL DEFAULT ('{}'),
        notes character varying(500),
        is_default boolean NOT NULL DEFAULT TRUE,
        customer_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_contacts PRIMARY KEY (id),
        CONSTRAINT fk_contacts_customers_customer_id FOREIGN KEY (customer_id) REFERENCES customers (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE discount_applied_to_categories (
        discount_id uuid NOT NULL,
        category_id uuid NOT NULL,
        CONSTRAINT pk_discount_applied_to_categories PRIMARY KEY (discount_id, category_id),
        CONSTRAINT fk_discount_applied_to_categories_categories_category_id FOREIGN KEY (category_id) REFERENCES categories (id) ON DELETE CASCADE,
        CONSTRAINT fk_discount_applied_to_categories_discounts_discount_id FOREIGN KEY (discount_id) REFERENCES discounts (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE merchant_categories (
        merchant_id uuid NOT NULL,
        category_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_merchant_categories PRIMARY KEY (merchant_id, category_id),
        CONSTRAINT fk_merchant_categories_categories_category_id FOREIGN KEY (category_id) REFERENCES categories (id) ON DELETE CASCADE,
        CONSTRAINT fk_merchant_categories_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE order_promotions (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(250) NOT NULL,
        start_date timestamp with time zone NOT NULL,
        end_date timestamp with time zone NOT NULL,
        merchant_id uuid NOT NULL,
        status character varying(50) NOT NULL DEFAULT ('NEW'),
        type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        max_quantity integer NOT NULL DEFAULT 0,
        min_spend numeric(19,2) NOT NULL DEFAULT 0.0,
        bundle_promotion_discount_type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        conditions jsonb NOT NULL DEFAULT ('[]'),
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_order_promotions PRIMARY KEY (id),
        CONSTRAINT fk_order_promotions_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE product_promotions (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(250) NOT NULL,
        start_date timestamp with time zone NOT NULL,
        end_date timestamp with time zone NOT NULL,
        merchant_id uuid NOT NULL,
        status character varying(50) NOT NULL DEFAULT ('NEW'),
        type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_product_promotions PRIMARY KEY (id),
        CONSTRAINT fk_product_promotions_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE products (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        slug character varying(150) NOT NULL,
        name character varying(100) NOT NULL,
        description text NOT NULL,
        list_price numeric(19,2),
        stock integer DEFAULT 0,
        merchant_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        has_discounts_applied boolean NOT NULL,
        CONSTRAINT pk_products PRIMARY KEY (id),
        CONSTRAINT fk_products_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE shop_collections (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(150) NOT NULL,
        parent_id uuid,
        merchant_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_shop_collections PRIMARY KEY (id),
        CONSTRAINT fk_shop_collections_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE,
        CONSTRAINT fk_shop_collections_shop_collections_parent_id FOREIGN KEY (parent_id) REFERENCES shop_collections (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE stores (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(150) NOT NULL,
        store_number text,
        description character varying(500) DEFAULT (''),
        phone_number text,
        is_active boolean NOT NULL DEFAULT TRUE,
        store_address text,
        merchant_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_stores PRIMARY KEY (id),
        CONSTRAINT fk_stores_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
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
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE discount_applied_to_products (
        discount_id uuid NOT NULL,
        product_id uuid NOT NULL,
        CONSTRAINT pk_discount_applied_to_products PRIMARY KEY (discount_id, product_id),
        CONSTRAINT fk_discount_applied_to_products_discounts_discount_id FOREIGN KEY (discount_id) REFERENCES discounts (id) ON DELETE CASCADE,
        CONSTRAINT fk_discount_applied_to_products_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE order_promotion_items (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        order_promotion_id uuid NOT NULL,
        product_id uuid NOT NULL,
        is_active boolean NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_order_promotion_items PRIMARY KEY (id),
        CONSTRAINT fk_order_promotion_items_order_promotions_order_promotion_id FOREIGN KEY (order_promotion_id) REFERENCES order_promotions (id) ON DELETE CASCADE,
        CONSTRAINT fk_order_promotion_items_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
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
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE product_variants (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        stock integer NOT NULL DEFAULT 0,
        price numeric NOT NULL DEFAULT 0.0,
        product_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_product_variants PRIMARY KEY (id),
        CONSTRAINT fk_product_variants_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE voucher_product (
        voucher_id uuid NOT NULL,
        product_id uuid NOT NULL,
        id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT pk_voucher_product PRIMARY KEY (voucher_id, product_id),
        CONSTRAINT fk_voucher_product_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE,
        CONSTRAINT fk_voucher_product_vouchers_voucher_id FOREIGN KEY (voucher_id) REFERENCES vouchers (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE shop_collection_product (
        shop_collection_id uuid NOT NULL,
        product_id uuid NOT NULL,
        id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT pk_shop_collection_product PRIMARY KEY (shop_collection_id, product_id),
        CONSTRAINT fk_shop_collection_product_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE,
        CONSTRAINT fk_shop_collection_product_shop_collections_shop_collection_id FOREIGN KEY (shop_collection_id) REFERENCES shop_collections (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE orders (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        order_number character varying(200) NOT NULL,
        customer_id uuid NOT NULL,
        phone_number character varying(20) NOT NULL,
        status character varying(50) NOT NULL DEFAULT ('TO_PAY'),
        payment_status character varying(50) NOT NULL DEFAULT ('UNPAID'),
        payment_method character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        total_price numeric(19,2) NOT NULL,
        total_discount_price numeric(19,2),
        total_item_price numeric(19,2),
        delivery_fee numeric(19,2),
        total_vat_price numeric(19,2),
        total_except_vat_price numeric(19,2),
        delivery_schedule timestamp with time zone NOT NULL,
        delivery_address text NOT NULL,
        store_id uuid NOT NULL,
        notes character varying(500),
        contact_id uuid,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_orders PRIMARY KEY (id),
        CONSTRAINT fk_orders_contacts_contact_id FOREIGN KEY (contact_id) REFERENCES contacts (id),
        CONSTRAINT fk_orders_customers_customer_id FOREIGN KEY (customer_id) REFERENCES customers (id) ON DELETE CASCADE,
        CONSTRAINT fk_orders_stores_store_id FOREIGN KEY (store_id) REFERENCES stores (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE order_promotion_sub_items (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        order_promotion_id uuid NOT NULL,
        product_id uuid NOT NULL,
        product_variant_id uuid,
        is_active boolean NOT NULL,
        type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        discount_price numeric(19,2) NOT NULL DEFAULT 0.0,
        discount_percentage numeric(19,2) NOT NULL DEFAULT 0.0,
        no_products_per_order_limit character varying(50) NOT NULL DEFAULT ('SPECIFIC'),
        max_items_per_order integer NOT NULL DEFAULT 1,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_order_promotion_sub_items PRIMARY KEY (id),
        CONSTRAINT fk_order_promotion_sub_items_order_promotions_order_promotion_ FOREIGN KEY (order_promotion_id) REFERENCES order_promotions (id) ON DELETE CASCADE,
        CONSTRAINT fk_order_promotion_sub_items_product_variants_product_variant_ FOREIGN KEY (product_variant_id) REFERENCES product_variants (id),
        CONSTRAINT fk_order_promotion_sub_items_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE product_promotion_items (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        product_promotion_id uuid NOT NULL,
        product_id uuid NOT NULL,
        product_variant_id uuid,
        is_active boolean NOT NULL,
        list_price numeric(19,2) NOT NULL DEFAULT 0.0,
        discount_price numeric(19,2) NOT NULL DEFAULT 0.0,
        discount_percentage numeric(19,2) NOT NULL DEFAULT 0.0,
        quantity integer NOT NULL DEFAULT 0,
        no_products_per_order_limit integer NOT NULL,
        max_items_per_order integer NOT NULL DEFAULT 0,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_product_promotion_items PRIMARY KEY (id),
        CONSTRAINT fk_product_promotion_items_product_promotions_product_promotio FOREIGN KEY (product_promotion_id) REFERENCES product_promotions (id) ON DELETE CASCADE,
        CONSTRAINT fk_product_promotion_items_product_variants_product_variant_id FOREIGN KEY (product_variant_id) REFERENCES product_variants (id) ON DELETE CASCADE,
        CONSTRAINT fk_product_promotion_items_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE product_variant_attribute_values (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        product_variant_id uuid NOT NULL,
        product_attribute_id uuid NOT NULL,
        value character varying(200) NOT NULL,
        attribute_value_id uuid,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_product_variant_attribute_values PRIMARY KEY (id),
        CONSTRAINT ak_product_variant_attribute_values_product_variant_id_product UNIQUE (product_variant_id, product_attribute_id),
        CONSTRAINT fk_product_variant_attribute_values_attribute_values_attribute FOREIGN KEY (attribute_value_id) REFERENCES attribute_values (id) ON DELETE CASCADE,
        CONSTRAINT fk_product_variant_attribute_values_product_attributes_product FOREIGN KEY (product_attribute_id) REFERENCES product_attributes (id) ON DELETE CASCADE,
        CONSTRAINT fk_product_variant_attribute_values_product_variants_product_v FOREIGN KEY (product_variant_id) REFERENCES product_variants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE order_items (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        order_id uuid NOT NULL,
        product_id uuid,
        product_variant_id uuid,
        quantity integer NOT NULL DEFAULT 0,
        unit_price numeric(19,2) NOT NULL,
        total_price numeric(19,2) NOT NULL,
        vat_price numeric(19,2),
        except_vat_price numeric(19,2),
        total_vat_price numeric(19,2),
        total_except_vat_price numeric(19,2),
        vat_percent real,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_order_items PRIMARY KEY (id),
        CONSTRAINT fk_order_items_orders_order_id FOREIGN KEY (order_id) REFERENCES orders (id) ON DELETE CASCADE,
        CONSTRAINT fk_order_items_product_variants_product_variant_id FOREIGN KEY (product_variant_id) REFERENCES product_variants (id) ON DELETE CASCADE,
        CONSTRAINT fk_order_items_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE order_status_trackings (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        order_id uuid NOT NULL,
        order_status character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_order_status_trackings PRIMARY KEY (id),
        CONSTRAINT fk_order_status_trackings_orders_order_id FOREIGN KEY (order_id) REFERENCES orders (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE payment_method_trackings (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        order_id uuid NOT NULL,
        payment_method character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        ref_id text,
        ref_code text,
        ref_name text,
        value numeric(19,2),
        base_value numeric(19,2),
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_payment_method_trackings PRIMARY KEY (id),
        CONSTRAINT fk_payment_method_trackings_orders_order_id FOREIGN KEY (order_id) REFERENCES orders (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE TABLE payment_status_trackings (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        order_id uuid NOT NULL,
        payment_status character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_payment_status_trackings PRIMARY KEY (id),
        CONSTRAINT fk_payment_status_trackings_orders_order_id FOREIGN KEY (order_id) REFERENCES orders (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_attribute_values_product_attribute_id ON attribute_values (product_attribute_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_categories_parent_id ON categories (parent_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_contacts_customer_id ON contacts (customer_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_discount_applied_to_categories_category_id ON discount_applied_to_categories (category_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_discount_applied_to_categories_discount_id ON discount_applied_to_categories (discount_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_discount_applied_to_products_discount_id ON discount_applied_to_products (discount_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_discount_applied_to_products_product_id ON discount_applied_to_products (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE UNIQUE INDEX ix_discounts_code ON discounts (code);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_discounts_discount_id ON discounts (discount_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_merchant_categories_category_id ON merchant_categories (category_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_items_order_id ON order_items (order_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_items_product_id ON order_items (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_items_product_variant_id ON order_items (product_variant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_promotion_items_order_promotion_id ON order_promotion_items (order_promotion_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_promotion_items_product_id ON order_promotion_items (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_promotion_sub_items_order_promotion_id ON order_promotion_sub_items (order_promotion_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_promotion_sub_items_product_id ON order_promotion_sub_items (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_promotion_sub_items_product_variant_id ON order_promotion_sub_items (product_variant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_promotions_merchant_id ON order_promotions (merchant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_order_status_trackings_order_id ON order_status_trackings (order_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_orders_contact_id ON orders (contact_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_orders_customer_id ON orders (customer_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE UNIQUE INDEX ix_orders_order_number ON orders (order_number);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_orders_store_id ON orders (store_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_payment_method_trackings_order_id ON payment_method_trackings (order_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_payment_status_trackings_order_id ON payment_status_trackings (order_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_product_product_attributes_product_attribute_id ON product_product_attributes (product_attribute_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_product_promotion_items_product_id ON product_promotion_items (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_product_promotion_items_product_promotion_id ON product_promotion_items (product_promotion_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_product_promotion_items_product_variant_id ON product_promotion_items (product_variant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_product_promotions_merchant_id ON product_promotions (merchant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_product_variant_attribute_values_attribute_value_id ON product_variant_attribute_values (attribute_value_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_product_variant_attribute_values_product_attribute_id ON product_variant_attribute_values (product_attribute_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_product_variants_product_id ON product_variants (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_products_merchant_id ON products (merchant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_shop_collection_product_product_id ON shop_collection_product (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_shop_collections_merchant_id ON shop_collections (merchant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_shop_collections_parent_id ON shop_collections (parent_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE UNIQUE INDEX ix_shop_collections_slug ON shop_collections (slug);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_stores_merchant_id ON stores (merchant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    CREATE INDEX ix_voucher_product_product_id ON voucher_product (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240920042822_Initialize') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240920042822_Initialize', '8.0.1');
    END IF;
END $EF$;
COMMIT;

