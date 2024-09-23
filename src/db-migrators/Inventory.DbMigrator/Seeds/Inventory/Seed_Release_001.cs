using System.Linq;
using System.Threading.Tasks;
using ECommerce.Inventory.Data;
using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.Inventory.DbMigrator.Seeds.Inventory
{
    public sealed class Seed_Release_001
    {
        public static async Task SeedAsync(
            IServiceProvider services,
            InventoryDbContext dbContext,
            ILogger<InventoryDbContext> logger)
        {
            await InitAttributes(services);
            await InitAttributeValues(services);
            await InitCategories(services);
            await InitMerchants(services);
        }

        private static async Task InitAttributes(IServiceProvider serviceProvider)
        {
            var attributes = new ProductAttribute[] {
                new("color"),
                new("size"),
                new("material"),
                new("weight")
            };

            await InitAttributes(serviceProvider, attributes);
        }

        private static async Task InitAttributes(IServiceProvider serviceProvider, params ProductAttribute[] attributes)
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

            foreach (var attribute in attributes)
            {
                if (await dbContext.ProductAttributes.AnyAsync(t => t.Name == attribute.Name))
                    continue;

                var newAttribute = new ProductAttribute(attribute.Name);
                newAttribute.SetPredefined();

                dbContext.ProductAttributes.Add(newAttribute);

                var result = await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InitAttributeValues(IServiceProvider serviceProvider)
        {
            var values = new AttributeValueInfo[] {
                // Values for 'color' attribute
                new("Red", "color"),
                new("Blue", "color"),
                new("Green", "color"),
                new("Black", "color"),
                new("White", "color"),

                // Values for 'size' attribute
                new("Small", "size"),
                new("Medium", "size"),
                new("Large", "size"),
                new("X-Large", "size"),

                // Values for 'material' attribute
                new("Cotton", "material"),
                new("Polyester", "material"),
                new("Leather", "material"),
                new("Silk", "material"),

                // Values for 'weight' attribute
                new("Light", "weight"),
                new("Medium", "weight"),
                new("Heavy", "weight"),
                new("Ultra-Light", "weight"),
            };
            await InitAttributeValues(serviceProvider, values);
        }

        private static async Task InitAttributeValues(IServiceProvider serviceProvider, params AttributeValueInfo[] values)
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

            var attributeNames = values.Select(t => t.AttributeName).Distinct();

            var attributes = await dbContext.ProductAttributes.Where(t => attributeNames.Contains(t.Name)).ToListAsync();

            foreach (var value in values)
            {
                var attribute = attributes.FirstOrDefault(t => t.Name == value.AttributeName);

                if (await dbContext.AttributeValues.AnyAsync(t => t.Value == value.Value &&
                        attribute != null && t.ProductAttributeId == attribute.Id))
                    continue;

                var newValue = new AttributeValue(value.Value);

                if (attribute != null)
                    newValue.SetAttribute(attribute.Id);

                dbContext.AttributeValues.Add(newValue);

                await dbContext.SaveChangesAsync();
            }
        }


        private static async Task InitCategories(IServiceProvider serviceProvider)
        {
            var categories = new CategoryInfo[] {
                new("Men Clothes", "men-clothes", new List<Category>
                {
                    new ("Jackets, Coats & Vests", "men-jackets-coats-vests", null, null),
                    new ("Suit Jackets & Blazers", "men-suit-jackets-blazers", null, null),
                    new ("Hoodies & Sweatshirts", "men-hoodies-sweatshirts", null, null),
                    new ("Jeans", "men-jeans", null, null),
                    new ("Pants/Suits", "men-pants-suits", null, null),
                    new ("Shorts", "men-shorts", null, null),
                    new ("Tops", "men-tops", null, null),
                    new ("Tanks", "men-tanks", null, null),
                    new ("Innerwear & Underwear", "men-innerwear-underwear", null, null),
                    new ("Sleepwear", "men-sleepwear", null, null),
                    new ("Sets", "men-sets", null, null),
                    new ("Socks", "men-socks", null, null),
                    new ("Traditional Wear", "men-traditional-wear", null, null),
                    new ("Costumes", "men-costumes", null, null),
                    new ("Occupational Attire", "men-occupational-attire", null, null),
                    new ("Others", "men-others", null, null),
                    new ("Men Jewelries", "men-jewelries", null, null),
                    new ("Eyewear", "men-eyewear", null, null),
                    new ("Belts", "men-belts", null, null),
                    new ("Neckties, Bow Ties & Cravats", "men-neckties-bow-ties-cravats", null, null),
                    new ("Additional Accessories", "men-additional-accessories", null, null)
                }),
                new("Women Clothes", "women-clothes", new List<Category>
                {
                    new Category("Pants & Leggings", "women-pants-leggings", null, null),
                    new Category("Shorts", "women-shorts", null, null),
                    new Category("Skirts", "women-skirts", null, null),
                    new Category("Jeans", "women-jeans", null, null),
                    new Category("Dresses", "women-dresses", null, null),
                    new Category("Wedding Dresses", "women-wedding-dresses", null, null),
                    new Category("Jumpsuits, Playsuits & Overalls", "women-jumpsuits-playsuits-overalls", null, null),
                    new Category("Jackets, Coats & Vests", "women-jackets-coats-vests", null, null),
                    new Category("Sweaters & Cardigans", "women-sweaters-cardigans", null, null),
                    new Category("Hoodies & Sweatshirts", "women-hoodies-sweatshirts", null, null),
                    new Category("Sets", "women-sets", null, null),
                    new Category("Lingerie & Underwear", "women-lingerie-underwear", null, null),
                    new Category("Sleepwear & Pajamas", "women-sleepwear-pajamas", null, null),
                    new Category("Tops", "women-tops", null, null),
                    new Category("Sportwear", "women-sportwear", null, null),
                    new Category("Maternity Wear", "women-maternity-wear", null, null),
                    new Category("Traditional Wear", "women-traditional-wear", null, null),
                    new Category("Costumes", "women-costumes", null, null),
                    new Category("Fabric", "women-fabric", null, null),
                    new Category("Socks & Stockings", "women-socks-stockings", null, null),
                    new Category("Others", "women-clothes-others", null, null),
                }),
                new("Men Bags", "men-bags", new List<Category>
                {
                    new Category("Backpacks", "men-backpacks", null, null),
                    new Category("Laptop Backpacks", "men-laptop-backpacks", null, null),
                    new Category("Laptop Bags & Cases", "men-laptop-bags-cases", null, null),
                    new Category("Laptop Sleeves", "men-laptop-sleeves", null, null),
                    new Category("Tote Bags", "men-tote-bags", null, null),
                    new Category("Briefcases", "men-briefcases", null, null),
                    new Category("Clutches", "men-clutches", null, null),
                    new Category("Waist Bags & Chest Bags", "men-waist-bags-chest-bags", null, null),
                    new Category("Crossbody & Shoulder Bags", "men-crossbody-shoulder-bags", null, null),
                    new Category("Wallets", "men-wallets", null, null),
                    new Category("Others", "men-bags-others", null, null),
                }),
                new("Women Bags", "women-bags", new List<Category>
                {
                    new Category("Backpacks", "women-backpacks", null, null),
                    new Category("Laptop Backpacks", "women-laptop-backpacks", null, null),
                    new Category("Clutches & Wristlets", "women-clutches-wristlets", null, null),
                    new Category("Waist Bags & Chest Bags", "women-waist-bags-chest-bags", null, null),
                    new Category("Tote Bags", "women-tote-bags", null, null),
                    new Category("Top-handle Bags", "women-top-handle-bags", null, null),
                    new Category("Crossbody & Shoulder Bags", "women-crossbody-shoulder-bags", null, null),
                    new Category("Wallets", "women-wallets", null, null),
                    new Category("Bag Accessories", "women-bag-accessories", null, null),
                    new Category("Others", "women-bags-others", null, null),
                }),
                new("Men shoes", "men-shoes", new List<Category>
                {
                    new Category("Boots", "men-shoes-boots", null, null),
                    new Category("Sneakers", "men-shoes-sneakers", null, null),
                    new Category("Slip Ons & Mules", "men-shoes-slip-ons-mules", null, null),
                    new Category("Loafers & Boat Shoes", "men-shoes-loafers-boat-shoes", null, null),
                    new Category("Oxfords & Lace-Ups", "men-shoes-oxfords-lace-ups", null, null),
                    new Category("Sandals & Flip Flops", "men-shoes-sandals-flip-flops", null, null),
                    new Category("Shoe Care & Accessories", "men-shoes-shoe-care-accessories", null, null),
                    new Category("Others", "men-shoes-others", null, null),
                }),
                new("Women shoes", "women-shoes", new List<Category>
                {
                    new Category("Boots", "women-shoes-boots", null, null),
                    new Category("Sneakers", "women-shoes-sneakers", null, null),
                    new Category("Flats", "women-shoes-flats", null, null),
                    new Category("Heels", "women-shoes-heels", null, null),
                    new Category("Wedges", "women-shoes-wedges", null, null),
                    new Category("Flat Sandals & Flip Flops", "women-shoes-flat-sandals-flip-flops", null, null),
                    new Category("Shoe Care & Accessories", "women-shoes-shoe-care-accessories", null, null),
                    new Category("Others", "women-shoes-others", null, null),
                }),
                new("Moms, Kids & Babies", "moms-kids-babies", new List<Category>
                {
                    new Category("Baby Travel Essentials", "mom-kid-baby-travel-essentials", null, null),
                    new Category("Feeding Essentials", "mom-kid-feeding-essentials", null, null),
                    new Category("Maternity Accessories", "mom-kid-maternity-accessories", null, null),
                    new Category("Maternity Healthcare", "mom-kid-maternity-healthcare", null, null),
                    new Category("Bath & Body Care", "mom-kid-bath-body-care", null, null),
                    new Category("Nursery", "mom-kid-nursery", null, null),
                    new Category("Baby Safety", "mom-kid-baby-safety", null, null),
                    new Category("Baby Food", "mom-kid-baby-food", null, null),
                    new Category("Baby Healthcare", "mom-kid-baby-healthcare", null, null),
                    new Category("Diapering & Potty", "mom-kid-diapering-potty", null, null),
                    new Category("Toys", "mom-kid-toys", null, null),
                    new Category("Gift Sets & Packages", "mom-kid-gift-sets-packages", null, null),
                    new Category("Others", "mom-kid-others", null, null),
                    new Category("Milk 24 months and ups", "mom-kid-milk-24-months-ups", null, null),
                    new Category("Milk Formula 0-24 months", "mom-kid-milk-formula-0-24-months", null, null),
                }),
                new("Kid Fashion", "kid-fashion", new List<Category>
                {
                    new Category("Boy Clothes", "kid-fashion-boy-clothes", null, null),
                    new Category("Girl Clothes", "kid-fashion-girl-clothes", null, null),
                    new Category("Boy Shoes", "kid-fashion-boy-shoes", null, null),
                    new Category("Girl Shoes", "kid-fashion-girl-shoes", null, null),
                    new Category("Others", "kid-fashion-others", null, null),
                    new Category("Baby Clothes", "kid-fashion-baby-clothes", null, null),
                    new Category("Baby Mittens & Footwear", "kid-fashion-baby-mittens-footwear", null, null),
                    new Category("Baby & Kids Accessories", "kid-fashion-baby-kids-accessories", null, null),
                }),
                new("Fashion Accessories", "fashion-accessories", new List<Category>
                {
                    new Category("Rings", "fashion-accessory-rings", null, null),
                    new Category("Earrings", "fashion-accessory-earrings", null, null),
                    new Category("Scarves & Shawls", "fashion-accessory-scarves-shawls", null, null),
                    new Category("Gloves", "fashion-accessory-gloves", null, null),
                    new Category("Hair Accessories", "fashion-accessory-hair-accessories", null, null),
                    new Category("Bracelets & Bangles", "fashion-accessory-bracelets-bangles", null, null),
                    new Category("Anklets", "fashion-accessory-anklets", null, null),
                    new Category("Hats & Caps", "fashion-accessory-hats-caps", null, null),
                    new Category("Necklaces", "fashion-accessory-necklaces", null, null),
                    new Category("Eyewear", "fashion-accessory-eyewear", null, null),
                    new Category("Investment Precious Metals", "fashion-accessory-investment-precious-metals", null, null),
                    new Category("Belts", "fashion-accessory-belts", null, null),
                    new Category("Neckties, Bow Ties & Cravats", "fashion-accessory-neckties-bow-ties-cravats", null, null),
                    new Category("Additional Accessories", "fashion-accessory-additional-accessories", null, null),
                    new Category("Accessories Sets & Packages", "fashion-accessory-accessories-sets-packages", null, null),
                    new Category("Others", "fashion-accessory-others", null, null),
                    new Category("Socks & Stockings", "fashion-accessory-socks-stockings", null, null),
                    new Category("Umbrella", "fashion-accessory-umbrella", null, null),
                }),
                new("Beauty", "beauty", new List<Category>
                {
                    new Category("Skincare", "beauty-skincare", null, null),
                    new Category("Bath & Body Care", "beauty-bath-body-care", null, null),
                    new Category("Makeup", "beauty-makeup", null, null),
                    new Category("Hair Care", "beauty-hair-care", null, null),
                    new Category("Beauty Tools & Accessories", "beauty-tools-accessories", null, null),
                    new Category("Oral Care", "beauty-oral-care", null, null),
                    new Category("Perfumes & Fragrances", "beauty-perfumes-fragrances", null, null),
                    new Category("Men's Care", "beauty-mens-care", null, null),
                    new Category("Others", "beauty-others", null, null),
                    new Category("Feminine Care", "beauty-feminine-care", null, null),
                    new Category("Beauty Sets & Packages", "beauty-sets-packages", null, null),
                }),
                new("Watches", "watches", new List<Category>
                {
                    new Category("Men Watches", "watches-men-watches", null, null),
                    new Category("Women Watches", "watches-women-watches", null, null),
                    new Category("Set & Couple Watches", "watches-set-couple-watches", null, null),
                    new Category("Kid Watches", "watches-kid-watches", null, null),
                    new Category("Watches Accessories", "watches-accessories", null, null),
                    new Category("Others", "watches-others", null, null),
                }),
                new("Mobile & Gadgets", "mobile-gadgets", new List<Category>
                {
                    new Category("Mobile Phones", "mobile-gadget-mobile-phones", null, null),
                    new Category("Tablets", "mobile-gadget-tablets", null, null),
                    new Category("Powerbanks", "mobile-gadget-powerbanks", null, null),
                    new Category("Batteries, Cables & Charger", "mobile-gadget-batteries-cables-charger", null, null),
                    new Category("Cases, Covers, & Skins", "mobile-gadget-cases-covers-skins", null, null),
                    new Category("Screen Protectors", "mobile-gadget-screen-protectors", null, null),
                    new Category("Phone Holders", "mobile-gadget-phone-holders", null, null),
                    new Category("Memory Cards", "mobile-gadget-memory-cards", null, null),
                    new Category("Sims", "mobile-gadget-sims", null, null),
                    new Category("Other Accessories", "mobile-gadget-other-accessories", null, null),
                    new Category("Other devices", "mobile-gadget-other-devices", null, null),
                }),
                new("Consumer Electronics", "consumer-electronics", new List<Category>
                {
                    new Category("Wearable Devices", "consumer-electronic-wearable-devices", null, null),
                    new Category("Tivi Accessories", "consumer-electronic-tivi-accessories", null, null),
                    new Category("Gaming & Console", "consumer-electronic-gaming-console", null, null),
                    new Category("Console Accessories", "consumer-electronic-console-accessories", null, null),
                    new Category("Video Games", "consumer-electronic-video-games", null, null),
                    new Category("Accessories and spare parts", "consumer-electronic-accessories-spare-parts", null, null),
                    new Category("Earphones", "consumer-electronic-earphones", null, null),
                    new Category("Audio", "consumer-electronic-audio", null, null),
                    new Category("Tivi", "consumer-electronic-tivi", null, null),
                    new Category("Tivi Box", "consumer-electronic-tivi-box", null, null),
                    new Category("Headphones", "consumer-electronic-headphones", null, null),
                }),
                new("Home & Living", "home-living", new List<Category>
                {
                    new Category("Bedding", "home-living-bedding", null, null),
                    new Category("Furniture", "home-living-furniture", null, null),
                    new Category("Home Decoration", "home-living-home-decoration", null, null),
                    new Category("Tools and Home improvement", "home-living-tools-home-improvement", null, null),
                    new Category("Kitchenware and food storage", "home-living-kitchenware-food-storage", null, null),
                    new Category("Lighting", "home-living-lighting", null, null),
                    new Category("Outdoor & Garden", "home-living-outdoor-garden", null, null),
                    new Category("Bathroom", "home-living-bathroom", null, null),
                    new Category("Regilious and Worship items", "home-living-regilious-worship-items", null, null),
                    new Category("Party supplies", "home-living-party-supplies", null, null),
                    new Category("Housekeeping and Laundry", "home-living-housekeeping-laundry", null, null),
                    new Category("Houseorganizers", "home-living-houseorganizers", null, null),
                    new Category("Drinkware", "home-living-drinkware", null, null),
                    new Category("Home Fragrance & Aromatherapy", "home-living-home-fragrance-aromatherapy", null, null),
                    new Category("Dinnerware", "home-living-dinnerware", null, null),
                }),
                new("Home Applications", "home-applications", new List<Category>
                {
                    new Category("Kitchen Appliances", "home-application-kitchen-appliances", null, null),
                    new Category("Large Appliance", "home-application-large-appliance", null, null),
                    new Category("Vacuums & Floor care", "home-application-vacuums-floor-care", null, null),
                    new Category("Air Conditioners & Fans", "home-application-air-conditioners-fans", null, null),
                    new Category("Garment Care", "home-application-garment-care", null, null),
                    new Category("Others", "home-application-others", null, null),
                    new Category("Blenders, Mixers & Grinders", "home-application-blenders-mixers-grinders", null, null),
                    new Category("Electric Cookers", "home-application-electric-cookers", null, null),
                }),
                new("Home Care", "home-care", new List<Category>
                {
                    new Category("Laundry", "home-care-laundry", null, null),
                    new Category("Toilet Paper", "home-care-toilet-paper", null, null),
                    new Category("Household Cleaning", "home-care-household-cleaning", null, null),
                    new Category("Dishwashing", "home-care-dishwashing", null, null),
                    new Category("Cleaning Tools", "home-care-cleaning-tools", null, null),
                    new Category("Air Fresheners", "home-care-air-fresheners", null, null),
                    new Category("Insect Killer", "home-care-insect-killer", null, null),
                    new Category("Food preservation", "home-care-food-preservation", null, null),
                    new Category("Trash Bags", "home-care-trash-bags", null, null),
                }),
                new("Tools & Home Improvement", "tools-home-improvement", new List<Category>
                {
                    new Category("Handtool", "tool-home-handtool", null, null),
                    new Category("Large tools and equipment", "tool-home-large-tools-equipment", null, null),
                    new Category("Electrical Circuitry & Parts", "tool-home-electrical-circuitry-parts", null, null),
                    new Category("Building and construction", "tool-home-building-construction", null, null),
                    new Category("Accessories", "tool-home-accessories", null, null),
                }),
                new("Sport & Outdoor", "sport-outdoor", new List<Category>
                {
                    new Category("Luggage", "sport-outdoor-luggage", null, null),
                    new Category("Travel Bags", "sport-outdoor-travel-bags", null, null),
                    new Category("Travel Accessories", "sport-outdoor-travel-accessories", null, null),
                    new Category("Sports & Outdoor Recreation Equipments", "sport-outdoor-sports-outdoor-recreation-equipments", null, null),
                    new Category("Sports Footwear", "sport-outdoor-sports-footwear", null, null),
                    new Category("Sports & Outdoor Apparels", "sport-outdoor-sports-outdoor-apparels", null, null),
                    new Category("Sports & Outdoor Accessories", "sport-outdoor-sports-outdoor-accessories", null, null),
                    new Category("Others", "sport-outdoor-others", null, null),
                }),
                new("Grocery", "grocery", new List<Category>
                {
                    new Category("Snacks", "grocery-snacks", null, null),
                    new Category("Convenience / Ready-to-eat", "grocery-convenience-ready-to-eat", null, null),
                    new Category("Food Staples", "grocery-food-staples", null, null),
                    new Category("Cooking Essentials", "grocery-cooking-essentials", null, null),
                    new Category("Baking Needs", "grocery-baking-needs", null, null),
                    new Category("Dairy & Eggs", "grocery-dairy-eggs", null, null),
                    new Category("Beverages", "grocery-beverages", null, null),
                    new Category("Breakfast Cereals & Spread", "grocery-breakfast-cereals-spread", null, null),
                    new Category("Bakery", "grocery-bakery", null, null),
                    new Category("Alcoholic Beverages", "grocery-alcoholic-beverages", null, null),
                    new Category("Gift Set & Hampers", "grocery-gift-set-hampers", null, null),
                    new Category("Fresh & Frozen Food", "grocery-fresh-frozen-food", null, null),
                    new Category("Others", "grocery-others", null, null),
                }),
                new("Toys", "toys", new List<Category>
                {
                    new Category("Hobbies & Collectibles", "toy-hobbies-collectibles", null, null),
                    new Category("Game Zone", "toy-game-zone", null, null),
                    new Category("Educational Toys", "toy-educational-toys", null, null),
                    new Category("Baby & Toddler Toys", "toy-baby-toddler-toys", null, null),
                    new Category("Action & Outdoor Toys", "toy-action-outdoor-toys", null, null),
                    new Category("Dolls & Stuffed Toys", "toy-dolls-stuffed-toys", null, null),
                }),
                new("Automotive", "automotive", new List<Category>
                {
                    new Category("Bike, E-bike", "automotive-bike-e-bike", null, null),
                    new Category("Motorbike", "automotive-motorbike", null, null),
                    new Category("Car", "automotive-car", null, null),
                    new Category("Helmets", "automotive-helmets", null, null),
                    new Category("Motorbike Accessories", "automotive-motorbike-accessories", null, null),
                    new Category("Bicycle & E-bike Accessories", "automotive-bicycle-e-bike-accessories", null, null),
                    new Category("Interior Accessories", "automotive-interior-accessories", null, null),
                    new Category("Automotive Oils & Lubes", "automotive-oils-lubes", null, null),
                    new Category("Auto Parts & Spares", "automotive-auto-parts-spares", null, null),
                    new Category("Motorbike Spare Parts", "automotive-motorbike-spare-parts", null, null),
                    new Category("Exterior Accessories", "automotive-exterior-accessories", null, null),
                    new Category("Automotive Care", "automotive-care", null, null),
                    new Category("Automotive Services", "automotive-services", null, null),
                }),
                new("Cameras", "cameras", new List<Category>
                {
                    new Category("Security Cameras & Systems", "camera-security-cameras-systems", null, null),
                    new Category("Memory Cards", "camera-memory-cards", null, null),
                    new Category("Lenses", "camera-lenses", null, null),
                    new Category("Camera Accessories", "camera-camera-accessories", null, null),
                    new Category("Drones", "camera-drones", null, null),
                }),
                new("Computer & Accessories", "computer-accessories", new List<Category>
                {
                    new Category("Desktop Computers", "computer-desktop-computers", null, null),
                    new Category("Monitors", "computer-monitors", null, null),
                    new Category("Desktop & Laptop Components", "computer-desktop-laptop-components", null, null),
                    new Category("Data Storage", "computer-data-storage", null, null),
                    new Category("Network Components", "computer-network-components", null, null),
                    new Category("Printers, Scanners & Projectors", "computer-printers-scanners-projectors", null, null),
                    new Category("Peripherals & Accessories", "computer-peripherals-accessories", null, null),
                    new Category("Laptops", "computer-laptops", null, null),
                    new Category("Others", "computer-others", null, null),
                    new Category("Gaming", "computer-gaming", null, null),
                }),
                new("Books & Stationery", "books-stationery", new List<Category>
                {
                    new Category("Domestic Books", "book-domestic-books", null, null),
                    new Category("Foreign Books", "book-foreign-books", null, null),
                    new Category("Gift & Wrapping", "book-gift-wrapping", null, null),
                    new Category("Writing & Correction", "book-writing-correction", null, null),
                    new Category("School & Office Supplies", "book-school-office-supplies", null, null),
                    new Category("Coloring & Arts", "book-coloring-arts", null, null),
                    new Category("Notebooks & Paper Products", "book-notebooks-paper-products", null, null),
                    new Category("Souvenirs", "book-souvenirs", null, null),
                    new Category("Music & Media", "book-music-media", null, null),
                }),
                new("Tickets, Vouchers & Services", "tickets-vouchers-services", new List<Category>
                {
                    new Category("F&B", "ticket-fnb", null, null),
                    new Category("Events & Attractions", "ticket-events-attractions", null, null),
                    new Category("Telco", "ticket-telco", null, null),
                    new Category("Beauty & Wellness", "ticket-beauty-wellness", null, null),
                    new Category("Transport", "ticket-transport", null, null),
                    new Category("Lessons & Workshops", "ticket-lessons-workshops", null, null),
                    new Category("Travel", "ticket-travel", null, null),
                    new Category("Shopping", "ticket-shopping", null, null),
                    new Category("Shopee", "ticket-shopee", null, null),
                    new Category("Utilities", "ticket-utilities", null, null),
                    new Category("Services", "ticket-services", null, null),
                }),
                new("Pets", "pets", new List<Category>
                {
                    new Category("Pet Food", "pet-pet-food", null, null),
                    new Category("Pet Accessories", "pet-pet-accessories", null, null),
                    new Category("Litter & Toilet", "pet-litter-toilet", null, null),
                    new Category("Pet Clothing & Accessories", "pet-pet-clothing-accessories", null, null),
                    new Category("Pet Healthcare", "pet-pet-healthcare", null, null),
                    new Category("Pet Grooming", "pet-pet-grooming", null, null),
                    new Category("Others", "pet-others", null, null),
                }),
                new("Health", "health", new List<Category>
                {
                    new Category("Medical Supplies", "health-medical-supplies", null, null),
                    new Category("Insect Repellents", "health-insect-repellents", null, null),
                    new Category("Food Supplement", "health-food-supplement", null, null),
                    new Category("Adult Diapers & Incontinence", "health-adult-diapers-incontinence", null, null),
                    new Category("Beauty Supplements", "health-beauty-supplements", null, null),
                    new Category("Sexual Wellness", "health-sexual-wellness", null, null),
                    new Category("Massage & Therapy Devices", "health-massage-therapy-devices", null, null),
                    new Category("Others", "health-others", null, null),
                }),
            };
            await InitCategories(serviceProvider, categories);
        }

        private static async Task InitCategories(IServiceProvider serviceProvider, params CategoryInfo[] categories)
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

            foreach (var category in categories)
            {
                if (await dbContext.Categories.AnyAsync(t => t.Name == category.Name))
                    continue;

                var newCategory = new Category(category.Name, category.Slug, null, null);

                if (category.ChildCategories != null)
                {
                    newCategory.AddChildren(category.ChildCategories);
                }

                dbContext.Categories.Add(newCategory);

                var result = await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InitMerchants(IServiceProvider serviceProvider)
        {
            var merchants = new MerchantInfo[] {
                new("POLOMANOR", "polomaner", "POLOMANOR Mall", "polomanor-mall"),
                new("GUTEK", "gutek", "GUTEK Mall", "gutek-mall"),
                new("Sói gear", "soi-gear", "Sói gear Yêu thích", "soi-gear-yeu-thich"),
                new("Trang sức bạc Miuu Silver", "trang-suc-bac-miuu-silver", "Miuu Silver Mall", "miuu-silver-mall"),
                new("Gấu bông vân anh", "gau-bong-van-anh", "Gấu bông vân anh yêu thích", "gau-bong-van-anh-yeu-thich"),
                new("Romano Vietnam", "romano-vietnam", "Romano Vietnam Mall", "romano-vietnam-mall")
            };

            await InitMerchants(serviceProvider, merchants);
        }

        private static async Task InitMerchants(IServiceProvider serviceProvider, params MerchantInfo[] merchants)
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

            foreach (var merchant in merchants)
            {
                if (await dbContext.Merchants.AnyAsync(t => t.Slug == merchant.Slug))
                    continue;

                var newMerchant = new Merchant(merchant.Name, merchant.Slug);

                var newStore = new Store(merchant.Store.Name, merchant.Store.Slug, newMerchant.Id);
                newMerchant.AddStore(newStore);

                dbContext.Merchants.Add(newMerchant);
                var result = await dbContext.SaveChangesAsync();
            }
        }

        internal class AttributeValueInfo(string value, string attributeName)
        {
            public string Value { get; set; } = value;
            public string AttributeName { get; set; } = attributeName;
        }

        internal class CategoryInfo(string name, string slug, List<Category>? childCategories)
        {
            public string Name { get; set; } = name;
            public string Slug { get; set; } = slug;
            public List<Category>? ChildCategories { get; set; } = childCategories;
        }

        internal class MerchantInfo(string name, string slug, string nameStore, string slugStore)
        {
            public string Name { get; set; } = name;
            public string Slug { get; set; } = slug;
            public bool IsActive { get; set; } = true;
            public StoreInfo Store { get; set; } = new StoreInfo(nameStore, slugStore);
        }

        internal class StoreInfo(string name, string slug)
        {
            public string Name { get; set; } = name;
            public string Slug { get; set; } = slug;
            public bool IsActive { get; set; } = true;
        }
    }
}
