namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;

    internal class DishesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Dishes.Any())
            {
                return;
            }

            List<Dish> dishes = new List<Dish>();

            // Hot drinks
            // 1
            dishes.Add(new Dish
            {
                Name = "Coffee Espresso",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/Coffee_Espresso.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Coffee Lavazza",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 30,
                DishImageUrl = "images/dishes/hotDrinks/Coffee_Lavazza.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "Cappucino",
                Price = 3.50M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/hotDrinks/Cappucino.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Vienna coffee",
                Price = 3.30M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 25,
                DishImageUrl = "images/dishes/hotDrinks/Vienna_coffee.png",
            });

            // 5
            dishes.Add(new Dish
            {
                Name = "Milk with cocoa",
                Price = 4.50M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/hotDrinks/Milk_with_cocoa.png",
            });

            // 6
            dishes.Add(new Dish
            {
                Name = "Milk with nescafe",
                Price = 4.50M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/hotDrinks/Milk_with_nescafe.png",
            });

            // 7
            dishes.Add(new Dish
            {
                Name = "Hot chocolate",
                Price = 5.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 30,
                DishImageUrl = "images/dishes/hotDrinks/Hot_chocolate.png",
            });

            // 8
            dishes.Add(new Dish
            {
                Name = "Tea",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/Tea.png",
            });

            // Cold drinks
            // 1
            dishes.Add(new Dish
            {
                Name = "Coca Cola",
                Price = 4.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/coldDrinks/Coca_Cola.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Soda",
                Price = 3.50M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/coldDrinks/Soda.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "Mineral water",
                Price = 4.50M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/coldDrinks/Mineral_water.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Iced tea",
                Price = 5.50M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 80,
                DishImageUrl = "images/dishes/coldDrinks/Iced_tea.png",
            });

            // 5
            dishes.Add(new Dish
            {
                Name = "Fruit juice",
                Price = 6.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 30,
                DishImageUrl = "images/dishes/coldDrinks/Fruit_juice.png",
            });

            // 6
            dishes.Add(new Dish
            {
                Name = "Smoothie",
                Price = 6.50M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 25,
                DishImageUrl = "images/dishes/coldDrinks/Smoothie.png",
            });

            // 7
            dishes.Add(new Dish
            {
                Name = "Nescafe Frappe",
                Price = 6.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/coldDrinks/Nescafe_Frappe.png",
            });

            // 8
            dishes.Add(new Dish
            {
                Name = "Shake",
                Price = 7.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/coldDrinks/Shake.png",
            });

            // Alcohol
            // 1
            dishes.Add(new Dish
            {
                Name = "Beer",
                Price = 6.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 500,
                DishImageUrl = "images/dishes/alcohol/Beer.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Red wine",
                Price = 7.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/alcohol/Red_wine.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "White wine",
                Price = 7.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/alcohol/White_wine.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Gin",
                Price = 8.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/alcohol/Gin.png",
            });

            // 5
            dishes.Add(new Dish
            {
                Name = "Tequila",
                Price = 8.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/alcohol/Tequila.png",
            });

            // 6
            dishes.Add(new Dish
            {
                Name = "Vodka",
                Price = 8.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/alcohol/Vodka.png",
            });

            // 7
            dishes.Add(new Dish
            {
                Name = "Whiskey",
                Price = 8.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/alcohol/Whiskey.png",
            });

            // 8
            dishes.Add(new Dish
            {
                Name = "Cocktail",
                Price = 10.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/alcohol/Cocktail.png",
            });

            // Appetizers

            // 1
            dishes.Add(new Dish
            {
                Name = "Air fryer loaded zucchini skins",
                Price = 18.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/appetizers/Air_fryer_loaded_zucchini_skins.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Antipasto bites",
                Price = 20.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 18,
                DishImageUrl = "images/dishes/appetizers/Antipasto_bites.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "Bacon wrapped water chestnuts",
                Price = 25.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/appetizers/Bacon_wrapped_water_chestnuts.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Baked brie",
                Price = 23.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 30,
                DishImageUrl = "images/dishes/appetizers/Baked_brie.png",
            });

            // 5
            dishes.Add(new Dish
            {
                Name = "Baked feta bites",
                Price = 33.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 27,
                DishImageUrl = "images/dishes/appetizers/Baked_feta_bites.png",
            });

            // 6
            dishes.Add(new Dish
            {
                Name = "Bang bang cauliflower",
                Price = 29.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 14,
                DishImageUrl = "images/dishes/appetizers/Bang_bang_cauliflower.png",
            });

            // 7
            dishes.Add(new Dish
            {
                Name = "Black eyed pea bruschetta",
                Price = 39.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 15,
                DishImageUrl = "images/dishes/appetizers/Black_eyed_pea_bruschetta.png",
            });

            // 8
            dishes.Add(new Dish
            {
                Name = "Brie asparagus prosciutto",
                Price = 27.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 18,
                DishImageUrl = "images/dishes/appetizers/Brie_asparagus_prosciutto.png",
            });

            // 9
            dishes.Add(new Dish
            {
                Name = "Brussels sprout chips",
                Price = 26.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 15,
                DishImageUrl = "images/dishes/appetizers/Brussels_sprout_chips.png",
            });

            // 10
            dishes.Add(new Dish
            {
                Name = "Cranberry brie bites",
                Price = 19.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 11,
                DishImageUrl = "images/dishes/appetizers/Cranberry_brie_bites.png",
            });

            // 11
            dishes.Add(new Dish
            {
                Name = "Garlic knots",
                Price = 22.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 19,
                DishImageUrl = "images/dishes/appetizers/Garlic_knots.png",
            });

            // 12
            dishes.Add(new Dish
            {
                Name = "Greek feta dip",
                Price = 29.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 9,
                DishImageUrl = "images/dishes/appetizers/Greek_feta_dip.png",
            });

            // 13
            dishes.Add(new Dish
            {
                Name = "Jalapeno popper crisps",
                Price = 25.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 33,
                DishImageUrl = "images/dishes/appetizers/Jalapeno_popper_crisps.png",
            });

            // 14
            dishes.Add(new Dish
            {
                Name = "Melon prosciutto skewers",
                Price = 34.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 30,
                DishImageUrl = "images/dishes/appetizers/Melon_prosciutto_skewers.png",
            });

            // 15
            dishes.Add(new Dish
            {
                Name = "Spinach artichoke brie",
                Price = 26.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 22,
                DishImageUrl = "images/dishes/appetizers/Spinach_artichoke_brie.png",
            });

            // 16
            dishes.Add(new Dish
            {
                Name = "Stuffed mushroom",
                Price = 23.00M,
                DishCategory = DishCategory.Appetizers,
                QuantityInStock = 29,
                DishImageUrl = "images/dishes/appetizers/Stuffed_mushroom.png",
            });

            // Gourmes
            // 1
            dishes.Add(new Dish
            {
                Name = "Beef Steak Black Angus Tritip",
                Price = 43.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 7,
                DishImageUrl = "images/dishes/gourme/Beef_Steak_Black_Angus_Tritip.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Chicken Scarparielo",
                Price = 37.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 13,
                DishImageUrl = "images/dishes/gourme/Chicken_Scarparielo.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "Foie gras cream",
                Price = 31.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 19,
                DishImageUrl = "images/dishes/gourme/Foie_gras_cream.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Foie gras souffle with red grape confit",
                Price = 36.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 16,
                DishImageUrl = "images/dishes/gourme/Foie_gras_souffle_with_red_grape_confit.png",
            });

            // 5
            dishes.Add(new Dish
            {
                Name = "Fresh raw blue swimming crab ocean gourme",
                Price = 56.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 11,
                DishImageUrl = "images/dishes/gourme/Fresh_raw_blue_swimming_crab_ocean_gourme.png",
            });

            // 6
            dishes.Add(new Dish
            {
                Name = "Fried fish fillet with rice salad",
                Price = 53.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 10,
                DishImageUrl = "images/dishes/gourme/Fried_fish_fillet_with_rice_salad.png",
            });

            // 7
            dishes.Add(new Dish
            {
                Name = "Gaspacho soup",
                Price = 33.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/gourme/Gaspacho_soup.png",
            });

            // 8
            dishes.Add(new Dish
            {
                Name = "Goose shanks with portobello forest mushrooms baked with new potatoes",
                Price = 38.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 15,
                DishImageUrl = "images/dishes/gourme/Goose_shanks_with_portobello_forest_mushrooms_baked_with_new_potatoes.png",
            });

            // 9
            dishes.Add(new Dish
            {
                Name = "Orange pancakes slices cheese with mint leaf",
                Price = 38.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 17,
                DishImageUrl = "images/dishes/gourme/Orange_pancakes_slices_cheese_with_mint_leaf.png",
            });

            // 10
            dishes.Add(new Dish
            {
                Name = "Salmon cheesecake with parmesan bread crisps, purslane and quail eggs",
                Price = 58.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 2,
                DishImageUrl = "images/dishes/gourme/Salmon_cheesecake_with_parmesan_bread_crisps_purslane_and_quail_eggs.png",
            });

            // 11
            dishes.Add(new Dish
            {
                Name = "Stuffed eggplant with tomatoes, carrots & cheese",
                Price = 44.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 12,
                DishImageUrl = "images/dishes/gourme/Stuffed_eggplant_with_tomatoes_carrots_cheese.png",
            });

            // 12
            dishes.Add(new Dish
            {
                Name = "Тurbot with shrimp mousse and vegetable spaghetti",
                Price = 47.00M,
                DishCategory = DishCategory.Gourmet,
                QuantityInStock = 22,
                DishImageUrl = "images/dishes/gourme/Тurbot_with_shrimp_mousse_and_vegetable_spaghetti.png",
            });

            // Salads
            // 1
            dishes.Add(new Dish
            {
                Name = "Asian slaw",
                Price = 27.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 39,
                DishImageUrl = "images/dishes/salads/Asian_slaw.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Broccoli salad",
                Price = 21.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 36,
                DishImageUrl = "images/dishes/salads/Broccoli_salad.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "Brussels salad",
                Price = 23.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 31,
                DishImageUrl = "images/dishes/salads/Brussels_salad.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Caprese salad",
                Price = 25.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 21,
                DishImageUrl = "images/dishes/salads/Caprese_salad.png",
            });

            // 5
            dishes.Add(new Dish
            {
                Name = "Cherry tabouleh",
                Price = 35.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 17,
                DishImageUrl = "images/dishes/salads/Cherry_tabouleh.png",
            });

            // 6
            dishes.Add(new Dish
            {
                Name = "Chimichurri potato salad",
                Price = 25.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 27,
                DishImageUrl = "images/dishes/salads/Chimichurri_potato_salad.png",
            });

            // 7
            dishes.Add(new Dish
            {
                Name = "Cobb salad with coconut bacon",
                Price = 44.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 9,
                DishImageUrl = "images/dishes/salads/Cobb_salad_with_coconut_bacon.png",
            });

            // 8
            dishes.Add(new Dish
            {
                Name = "Couscous salad",
                Price = 33.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 19,
                DishImageUrl = "images/dishes/salads/Couscous_salad.png",
            });

            // 9
            dishes.Add(new Dish
            {
                Name = "Heirloom tomato fattoush",
                Price = 29.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 18,
                DishImageUrl = "images/dishes/salads/Heirloom_tomato_fattoush.png",
            });

            // 10
            dishes.Add(new Dish
            {
                Name = "Kale salad with carrot ginger dressing",
                Price = 26.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 13,
                DishImageUrl = "images/dishes/salads/Kale_salad_with_carrot_ginger_dressing.png",
            });

            // 11
            dishes.Add(new Dish
            {
                Name = "Mexican street corn salad",
                Price = 35.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/salads/Mexican_street_corn_salad.png",
            });

            // 12
            dishes.Add(new Dish
            {
                Name = "Orzo salad",
                Price = 37.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 22,
                DishImageUrl = "images/dishes/salads/Orzo_salad.png",
            });

            // 13
            dishes.Add(new Dish
            {
                Name = "Pasta salad",
                Price = 35.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 24,
                DishImageUrl = "images/dishes/salads/Pasta_salad.png",
            });

            // 14
            dishes.Add(new Dish
            {
                Name = "Romain wedges",
                Price = 28.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 28,
                DishImageUrl = "images/dishes/salads/Romain_wedges.png",
            });

            // 15
            dishes.Add(new Dish
            {
                Name = "Soba noodles",
                Price = 30.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 12,
                DishImageUrl = "images/dishes/salads/Soba_noodles.png",
            });

            // 16
            dishes.Add(new Dish
            {
                Name = "Taco salad",
                Price = 26.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 16,
                DishImageUrl = "images/dishes/salads/Taco_salad.png",
            });

            // 17
            dishes.Add(new Dish
            {
                Name = "Tomato salad",
                Price = 23.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 14,
                DishImageUrl = "images/dishes/salads/Tomato_salad.png",
            });

            // 18
            dishes.Add(new Dish
            {
                Name = "Watermelon tomato salad",
                Price = 25.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 14,
                DishImageUrl = "images/dishes/salads/Watermelon_tomato_salad.png",
            });

            // Main courses
            // 1
            dishes.Add(new Dish
            {
                Name = "Broken lasagna with parmesan and peas",
                Price = 45.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 10,
                DishImageUrl = "images/dishes/mainCourses/Broken_lasagna_with_parmesan_and_peas.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Cauliflower bolognese",
                Price = 50.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 5,
                DishImageUrl = "images/dishes/mainCourses/Cauliflower_bolognese.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "Chard wrapped fish with lemon and olive",
                Price = 55.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 7,
                DishImageUrl = "images/dishes/mainCourses/Chard_wrapped_fish_with_lemon_and_olive.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Charred chicken with sweet potatoes and oranges",
                Price = 49.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 17,
                DishImageUrl = "images/dishes/mainCourses/Charred_chicken_with_sweet_potatoes_and_oranges.png",
            });

            // 5
            dishes.Add(new Dish
            {
                Name = "Chicken and potato gratin with brown butter cream",
                Price = 47.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 27,
                DishImageUrl = "images/dishes/mainCourses/Chicken_and_potato_gratin_with_brown_butter_cream.png",
            });

            // 6
            dishes.Add(new Dish
            {
                Name = "Chicken legs with grapes and fennel",
                Price = 49.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 27,
                DishImageUrl = "images/dishes/mainCourses/Chicken_legs_with_grapes_and_fennel.png",
            });

            // 7
            dishes.Add(new Dish
            {
                Name = "Clam toasts with pancetta",
                Price = 69.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 5,
                DishImageUrl = "images/dishes/mainCourses/Clam_toasts_with_pancetta.png",
            });

            // 8
            dishes.Add(new Dish
            {
                Name = "Coq au vin with cocoa powder",
                Price = 49.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 25,
                DishImageUrl = "images/dishes/mainCourses/Coq_au_vin_with_cocoa_powder.png",
            });

            // 9
            dishes.Add(new Dish
            {
                Name = "Creamy squash risotto with toasted pepitas",
                Price = 43.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 27,
                DishImageUrl = "images/dishes/mainCourses/Creamy_squash_risotto_with_toasted_pepitas.png",
            });

            // 10
            dishes.Add(new Dish
            {
                Name = "Crispy chicken thighs with spring vegetables",
                Price = 49.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 23,
                DishImageUrl = "images/dishes/mainCourses/Crispy_chicken_thighs_with_spring_vegetables.png",
            });

            // 11
            dishes.Add(new Dish
            {
                Name = "Easy lamb tagine with pomegranate",
                Price = 48.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 22,
                DishImageUrl = "images/dishes/mainCourses/Easy_lamb_tagine_with_pomegranate.png",
            });

            // 12
            dishes.Add(new Dish
            {
                Name = "Fish tacos al pastor",
                Price = 46.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 26,
                DishImageUrl = "images/dishes/mainCourses/Fish_tacos_al_pastor.png",
            });

            // 13
            dishes.Add(new Dish
            {
                Name = "Fried whole fish with tomatillo sauce",
                Price = 47.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 22,
                DishImageUrl = "images/dishes/mainCourses/Fried_whole_fish_with_tomatillo_sauce.png",
            });

            // 14
            dishes.Add(new Dish
            {
                Name = "Frogmore stew",
                Price = 57.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 2,
                DishImageUrl = "images/dishes/mainCourses/Frogmore_stew.png",
            });

            // 15
            dishes.Add(new Dish
            {
                Name = "Gado Gado",
                Price = 39.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 27,
                DishImageUrl = "images/dishes/mainCourses/Gado_Gado.png",
            });

            // 16
            dishes.Add(new Dish
            {
                Name = "Grand Aioli",
                Price = 40.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 37,
                DishImageUrl = "images/dishes/mainCourses/Grand_Aioli.png",
            });

            // 17
            dishes.Add(new Dish
            {
                Name = "Grilled jerk tofu and plantains with mango salsa",
                Price = 44.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 22,
                DishImageUrl = "images/dishes/mainCourses/Grilled_jerk_tofu_and_plantains_with_mango_salsa.png",
            });

            // 18
            dishes.Add(new Dish
            {
                Name = "Grilled pizza",
                Price = 45.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 32,
                DishImageUrl = "images/dishes/mainCourses/Grilled_pizza.png",
            });

            // 19
            dishes.Add(new Dish
            {
                Name = "Grilled pork spareribs with soda bottle barbecue sauce",
                Price = 49.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 17,
                DishImageUrl = "images/dishes/mainCourses/Grilled_pork_spareribs_with_soda_bottle_barbecue_sauce.png",
            });

            // 20
            dishes.Add(new Dish
            {
                Name = "Grilled scallops with creamed corn",
                Price = 41.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 37,
                DishImageUrl = "images/dishes/mainCourses/Grilled_scallops_with_creamed_corn.png",
            });

            // 21
            dishes.Add(new Dish
            {
                Name = "Habanero shrimp skewers",
                Price = 61.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 3,
                DishImageUrl = "images/dishes/mainCourses/Habanero_shrimp_skewers.png",
            });

            // 22
            dishes.Add(new Dish
            {
                Name = "Instant pot lamb haleem",
                Price = 51.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 23,
                DishImageUrl = "images/dishes/mainCourses/Instant_pot_lamb_haleem.png",
            });

            // 23
            dishes.Add(new Dish
            {
                Name = "Lamb chops with polenta and grilled scallion sauce",
                Price = 53.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 25,
                DishImageUrl = "images/dishes/mainCourses/Lamb_chops_with_polenta_and_grilled_scallion_sauce.png",
            });

            // 24
            dishes.Add(new Dish
            {
                Name = "Mast Biryani",
                Price = 50.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/mainCourses/Mast_Biryani.png",
            });

            // 25
            dishes.Add(new Dish
            {
                Name = "Mushrooms with bearnaise yogurt",
                Price = 44.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 25,
                DishImageUrl = "images/dishes/mainCourses/Mushrooms_with_bearnaise_yogurt.png",
            });

            // 26
            dishes.Add(new Dish
            {
                Name = "Pork chops with celery and almond salad",
                Price = 46.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 28,
                DishImageUrl = "images/dishes/mainCourses/Pork_chops_with_celery_and_almond_salad.png",
            });

            // 27
            dishes.Add(new Dish
            {
                Name = "Pork chops with fig and grape agrodolce",
                Price = 46.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 26,
                DishImageUrl = "images/dishes/mainCourses/Pork_chops_with_fig_and_grape_agrodolce.png",
            });

            // 28
            dishes.Add(new Dish
            {
                Name = "Pork shoulder with pineapple and sesame broccoli",
                Price = 49.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 19,
                DishImageUrl = "images/dishes/mainCourses/Pork_shoulder_with_pineapple-and_sesame_broccoli.png",
            });

            // 29
            dishes.Add(new Dish
            {
                Name = "Potato flake gnocchi",
                Price = 41.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 39,
                DishImageUrl = "images/dishes/mainCourses/Potato_flake_gnocchi.png",
            });

            // 30
            dishes.Add(new Dish
            {
                Name = "Pozole verde con hongos",
                Price = 50.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 13,
                DishImageUrl = "images/dishes/mainCourses/Pozole_verde_con_hongos.png",
            });

            // 31
            dishes.Add(new Dish
            {
                Name = "Roasted salmon with green herbs",
                Price = 60.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 23,
                DishImageUrl = "images/dishes/mainCourses/Roasted_salmon_with_green_herbs.png",
            });

            // 32
            dishes.Add(new Dish
            {
                Name = "Seared scallops with basil risotto",
                Price = 46.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 33,
                DishImageUrl = "images/dishes/mainCourses/Seared_scallops_with_basil_risotto.png",
            });

            // 33
            dishes.Add(new Dish
            {
                Name = "Seared scallops with brown butter and lemon pan sauce",
                Price = 48.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 31,
                DishImageUrl = "images/dishes/mainCourses/Seared_scallops_with_brown_butter_and_lemon_pan_sauce.png",
            });

            // 34
            dishes.Add(new Dish
            {
                Name = "Slow cooker pork shoulder with zesty basil sauce",
                Price = 49.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 34,
                DishImageUrl = "images/dishes/mainCourses/Slow_cooker_pork_shoulder_with_zesty_basil_sauce.png",
            });

            // 35
            dishes.Add(new Dish
            {
                Name = "Spicy lobster pasta",
                Price = 39.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 29,
                DishImageUrl = "images/dishes/mainCourses/Spicy_lobster_pasta.png",
            });

            // 36
            dishes.Add(new Dish
            {
                Name = "Spicy tamarind glazed grilled chicken wings",
                Price = 44.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 24,
                DishImageUrl = "images/dishes/mainCourses/Spicy_tamarind_glazed_grilled_chicken_wings.png",
            });

            // 37
            dishes.Add(new Dish
            {
                Name = "Spring risotto",
                Price = 34.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 36,
                DishImageUrl = "images/dishes/mainCourses/Spring_risotto.png",
            });

            // 38
            dishes.Add(new Dish
            {
                Name = "Sri Lankan cashew curry",
                Price = 39.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 3,
                DishImageUrl = "images/dishes/mainCourses/Sri_Lankan_cashew_curry.png",
            });

            // 39
            dishes.Add(new Dish
            {
                Name = "Stuffed eggplants and zucchini in a rich tomato sauce",
                Price = 33.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 23,
                DishImageUrl = "images/dishes/mainCourses/Stuffed_eggplants_and_zucchini_in_a_rich_tomato_sauce.png",
            });

            // 40
            dishes.Add(new Dish
            {
                Name = "Tamales con elote y chile poblano",
                Price = 28.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 43,
                DishImageUrl = "images/dishes/mainCourses/Tamales_con_elote_y_chile_poblano.png",
            });

            // 41
            dishes.Add(new Dish
            {
                Name = "Tamarind glazed black bass with coconut herb salad",
                Price = 48.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 8,
                DishImageUrl = "images/dishes/mainCourses/Tamarind_glazed_black_bass_with_coconut_herb_salad.png",
            });

            // 42
            dishes.Add(new Dish
            {
                Name = "Tomato and roasted garlic pie",
                Price = 35.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 28,
                DishImageUrl = "images/dishes/mainCourses/Tomato_and_roasted_garlic_pie.png",
            });

            // 43
            dishes.Add(new Dish
            {
                Name = "Tomato tart with chickpea crumble",
                Price = 36.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 26,
                DishImageUrl = "images/dishes/mainCourses/Tomato_tart_with_chickpea_crumble.png",
            });

            // 44
            dishes.Add(new Dish
            {
                Name = "Wild mushroom and parsnip ragout with cheesy polenta",
                Price = 44.00M,
                DishCategory = DishCategory.MainCourses,
                QuantityInStock = 24,
                DishImageUrl = "images/dishes/mainCourses/Wild_mushroom_and_parsnip_ragout_with_cheesy_polenta.png",
            });

            // Desserts
            // 1
            dishes.Add(new Dish
            {
                Name = "Banana_pudding",
                Price = 24.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 34,
                DishImageUrl = "images/dishes/desserts/Banana_pudding.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Caramel apple tart",
                Price = 27.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 33,
                DishImageUrl = "images/dishes/desserts/Caramel_apple_tart.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "Carrot cake dip",
                Price = 23.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 30,
                DishImageUrl = "images/dishes/desserts/Carrot_cake_dip.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Chocolate almond bark",
                Price = 29.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 29,
                DishImageUrl = "images/dishes/desserts/Chocolate_almond_bark.png",
            });

            // 5
            dishes.Add(new Dish
            {
                Name = "Chocolate covered cherries",
                Price = 25.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 25,
                DishImageUrl = "images/dishes/desserts/Chocolate_covered_cherries.png",
            });

            // 6
            dishes.Add(new Dish
            {
                Name = "Chocolate fluffernutter pie",
                Price = 31.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 22,
                DishImageUrl = "images/dishes/desserts/Chocolate_fluffernutter_pie.png",
            });

            // 7
            dishes.Add(new Dish
            {
                Name = "Chocolate pots de creme",
                Price = 32.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 24,
                DishImageUrl = "images/dishes/desserts/Chocolate_pots_de_creme.png",
            });

            // 8
            dishes.Add(new Dish
            {
                Name = "Confetti squares",
                Price = 29.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 28,
                DishImageUrl = "images/dishes/desserts/Confetti_squares.png",
            });

            // 9
            dishes.Add(new Dish
            {
                Name = "Cookie dough fudge",
                Price = 32.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 26,
                DishImageUrl = "images/dishes/desserts/Cookie_dough_fudge.png",
            });

            // 10
            dishes.Add(new Dish
            {
                Name = "Crepes",
                Price = 26.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 35,
                DishImageUrl = "images/dishes/desserts/Crepes.png",
            });

            // 11
            dishes.Add(new Dish
            {
                Name = "Fried ice cream",
                Price = 38.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 26,
                DishImageUrl = "images/dishes/desserts/Fried_ice_cream.png",
            });

            // 12
            dishes.Add(new Dish
            {
                Name = "Frozen samoa pie",
                Price = 31.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 29,
                DishImageUrl = "images/dishes/desserts/Frozen_samoa_pie.png",
            });

            // 13
            dishes.Add(new Dish
            {
                Name = "Key lime pie mousse",
                Price = 31.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 23,
                DishImageUrl = "images/dishes/desserts/Key_lime_pie_mousse.png",
            });

            // 14
            dishes.Add(new Dish
            {
                Name = "Magic cookie bars",
                Price = 35.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 18,
                DishImageUrl = "images/dishes/desserts/Magic_cookie_bars.png",
            });

            // 15
            dishes.Add(new Dish
            {
                Name = "No bake blackberry cheesecake bars",
                Price = 33.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 19,
                DishImageUrl = "images/dishes/desserts/No_bake_blackberry_cheesecake_bars.png",
            });

            // 16
            dishes.Add(new Dish
            {
                Name = "Oreo cookie skillet",
                Price = 38.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 29,
                DishImageUrl = "images/dishes/desserts/Oreo_cookie_skillet.png",
            });

            // 17
            dishes.Add(new Dish
            {
                Name = "Oreo truffles",
                Price = 38.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 28,
                DishImageUrl = "images/dishes/desserts/Oreo_truffles.png",
            });

            // 18
            dishes.Add(new Dish
            {
                Name = "Peach galette",
                Price = 25.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 38,
                DishImageUrl = "images/dishes/desserts/Peach_galette.png",
            });

            // 19
            dishes.Add(new Dish
            {
                Name = "Peanut butter dessert lasagna",
                Price = 29.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 69,
                DishImageUrl = "images/dishes/desserts/Peanut_butter_dessert_lasagna.png",
            });

            // 20
            dishes.Add(new Dish
            {
                Name = "Pistachio cake",
                Price = 31.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 39,
                DishImageUrl = "images/dishes/desserts/Pistachio_cake.png",
            });

            // 21
            dishes.Add(new Dish
            {
                Name = "Profiteroles",
                Price = 33.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 36,
                DishImageUrl = "images/dishes/desserts/Profiteroles.png",
            });

            // 22
            dishes.Add(new Dish
            {
                Name = "Samoa dessert lasagna",
                Price = 39.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 26,
                DishImageUrl = "images/dishes/desserts/Samoa_dessert_lasagna.png",
            });

            // 23
            dishes.Add(new Dish
            {
                Name = "Shoofly pie",
                Price = 25.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 46,
                DishImageUrl = "images/dishes/desserts/Shoofly_pie.png",
            });

            // 24
            dishes.Add(new Dish
            {
                Name = "Strawberry cheesecake pie",
                Price = 28.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 41,
                DishImageUrl = "images/dishes/desserts/Strawberry_cheesecake_pie.png",
            });

            // 25
            dishes.Add(new Dish
            {
                Name = "Stroopwafel milkshake",
                Price = 21.00M,
                DishCategory = DishCategory.Desserts,
                QuantityInStock = 51,
                DishImageUrl = "images/dishes/desserts/Stroopwafel_milkshake.png",
            });

            await dbContext.Dishes.AddRangeAsync(dishes);
            await dbContext.SaveChangesAsync();
        }
    }
}
