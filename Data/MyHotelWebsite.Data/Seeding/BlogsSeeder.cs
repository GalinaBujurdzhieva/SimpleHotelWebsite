namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;

    internal class BlogsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Blogs.Any())
            {
                return;
            }

            List<Blog> blogs = new List<Blog>();

            // 1
            blogs.Add(new Blog
            {
                Title = "Our newly equipped \"power place\" Gym with the latest fitness equipment",
                Content = "In our newly equipped fitness room you can get your circulation going, strengthen your muscles and do something good for your body. With high-quality and professional fitness equipment from Technogym, we offer you an extended range of sports. Endurance machines that are suitable for both light endurance training or a warm-up, as well as for a sweat-inducing and strengthening cardio workout. On the treadmill you can do a running workout that is easy on your joints, and the elliptical trainer provides a varied and effective whole-body workout. With the Concept 2 rowing machine and a bicycle, we provide you with professional indoor fitness equipment for the highest fitness demands. During your workout, you can connect to your Bluetooth headphones, on the internet-enabled equipment, and listen to your favourite music, watch a film or put together your individual workout with the free mobile phone app from Technogym.\r\n\r\nWith the strength station, you professionally train the muscles throughout your body. Various exercises as well as a wall bars and extensive accessories define and tone the body and make fitness hearts beat faster. A dumbbell rack and the matching dumbbell set guarantee professional dumbbell and strength training. A dumbbell bar is the ideal addition to our training station and perfects the gym equipment.\r\n\r\nA fascia set and a yoga mat are available for the perfect finish to release tension and loosen up the body again after your workout.",
                BlogImageUrl = "images/blogs/1.png",
            });

            // 2
            blogs.Add(new Blog
            {
                Title = "New buffet room\r\nOur new highlight",
                Content = "An interplay of form, colour and material is essential for creating a harmonious room effect. Together with our architect Thomas Bertalan, we have developed an authentic and highly distinctive concept. Tailor-made production from master craftsmen and quality are our top priorities. The refrigeration system is operated with water, a natural refrigerant. Whether in process cooling or food handling, cooling systems with natural refrigerants are becoming increasingly important, protect the environment and require less energy consumption. With the intelligent telecommunication software of HACCP remote maintenance, all connected systems can be checked and controlled around the clock. With a local specialist company, we were able to combine everything from one source, from concept planning, carpentry, as well as refrigeration and air conditioning technology, and optimally implement our philosophy of R50, with love for the region and a sense of sustainability.",
                BlogImageUrl = "images/blogs/2.png",
            });

            // 3
            blogs.Add(new Blog
            {
                Title = "Gentle holiday in the snow\r\nSustainability in winter holidays",
                Content = "Sustainability is a major issue for many people today. Also we are of this opinion and would like to pay conscious attention to our nature and environment, and deal with it better. We have 4 tips for you so that you too can enjoy the snow in the Stubaital this winter during your holiday and at the same time have a good conscience in matters of environment and nature.\r\n1. The right area\r\nChoose a snow-sure area - as well as our Stubaital, because if you want to experience snow fun in harmony with nature, you should go on holiday where as much natural snow as possible and few snow cannons are used.\r\n2. Sustainable variety\r\nWhat activities can be undertaken in winter that do not change or negatively affect nature? We have the answer for you. Hiking in the snow and then tobogganing with the whole family or on snowshoes through the snow-covered landscape, you can discover nature from a completely different and above all sustainable side. Cross-country skiing and ski touring also offer an entertaining change.\r\n3. Sustainable arrival\r\nOne of the most popular means of arrival is our car, it is comfortable and especially for a winter holiday one always has some luggage. But have you ever thought about an environmentally friendly way to get there by public transport? - Nature will thank you for it! We are happy to pick you up with our shuttle from the train or bus station. With public transport you can get from A to Z, as well as to the ski areas, stress-free and comfortable.\r\n4. Sustainable and ecological accommodation\r\nTo preserve an intact nature also for our descendants is close to our heart. That is why we obtain 100% of the energy we need to run our nature hotel in Tyrol from Tiroler Wasserkraft AG with the aid of state-of-the-art technology (such as groundwater heat pumps, heat exchangers, waste heat utilisation or monitoring of consumption electricity). Thus, an unbeatable 95% of our energy requirements come from CO2-free, renewable and nuclear-free sources. Optimised waste separation is just as much a matter of course for us as the use of ecologically valuable materials. We use seasonal and regional products and traditional delicacies, which are produced within a radius of 50 kilometres around the hotel and are typical for this region. For these and many other reasons we have been awarded the coveted Austrian and European Ecolabel. Come and see for yourself!",
                BlogImageUrl = "images/blogs/3.png",
            });

            // 4
            blogs.Add(new Blog
            {
                Title = "Shopping in Nina’s farm shop …\r\n… and taking home a piece of your holiday",
                Content = "At our herb farm, we are always working on new creations. Here you can look over our shoulder while we make our homemade products and take holiday memories home with you.\r\nSuccessful hiking holidays in the Alps are not complete without a souvenir to take home with you. In Nina's farm shop, you will find a large selection of home-made and regional products – from typical bread and smoked bacon to cheese that matures on our alp. Our guests can get a look at how the homemade products are produced and find out interesting facts about the use of herbs and the effect they have in special teas. Another highlight in our hotel in Tyrol are our tasting evenings in the herb farm. We regularly present food and other agricultural products or special dishes that are produced within a radius of 50 kilometres around our hotel in Neustift in the Stubai Valley and are typical for our region. You will experience an amazing variety of pleasures, which will always lead you to new discoveries – a new taste, a new tradition, a new delicacy.  Yummy, so delicious! We create great holiday memories and take them home with us!",
                BlogImageUrl = "images/blogs/4.png",
            });

            // 5
            blogs.Add(new Blog
            {
                Title = "In the footsteps of the Habsburgs\r\nA great day for the whole family in Innsbruck",
                Content = "The most famous attraction in Innsbruck is the Golden Roof, but there are many other highlights just waiting to be discovered.\r\nInnsbruck, the picturesque capital of Tyrol, is only 30 minutes away from our hotel. It is the perfect place for the whole family to spend a great day in the historic old town with modern flair. In addition to the famous Golden Roof, which Emperor Maximilian I had built for his wedding and which is covered with 2,657 fire-gilt shingles, there are many other great museums and historical sites to discover. We recommend the Imperial Hofburg or the Triumphpforte, for example. You will also find plenty of culinary hotspots and shopping opportunities in the historic Old Town. A tip for you: Visit the Kaufhaus TYROL. Here you will find a wide variety of shops and certainly the right thing for every taste. As soon as you return from your day trip to our hotel in Tyrol in the Stubaital, we will pamper you with moments of relaxing wellness and an excellent dinner for real gourmets. Amazing, right?",
                BlogImageUrl = "images/blogs/5.png",
            });

            // 6
            blogs.Add(new Blog
            {
                Title = "A great holiday\r\nGet to know our mountain sheep, rabbits, and fallow deer",
                Content = "Children love animals. Petting them is one of their most beautiful holiday memories. With us, young and old animal lovers have the opportunity to do so.\r\nOur hotel in Neustift in the Stubaital is anything but ordinary, because here with us you will definitely have a great time. Already from the distance, you can hear the beautiful roaring of our fallow deer, which sounds like music in your ears. They are the pride and joy of Johann Gleirscher, in whose life animals have always played an important role. Sharing this love for animals with his guests is particularly close to his heart. That is why he has built a fallow deer enclosure near our hotel in the Stubaital, where you can not only observe the graceful animals, but also feed them. You can also get up close and personal with our Tyrolean mountain sheep, which have found a cosy home in the adjacent stable of our Kräuterhof. They like to be petted just as much as our rabbits, which are especially popular with our little guests. Unforgettable holiday experiences are waiting for you in the Stubaital.",
                BlogImageUrl = "images/blogs/6.png",
            });

            // 7
            blogs.Add(new Blog
            {
                Title = "The Pinnistal: an unspoilt natural beauty\r\nOn the Meditation Trail",
                Content = "The Meditation Trail in the Pinnistal is one of the most famous hiking paths in Tyrol.\r\nUnspoilt, wild, and incredibly beautiful – this is the best way to describe the Austrian Pinnistal. Two impressive rock formations meet here: limestone and primary rock. Margret Gleirschler’s heart beats for this very special place in Tyrol. She knows every nook and cranny – and yet she is always enchanted by the beauty of this idyllic valley. If you would like to explore it as well, we recommend following the idyllic Meditation Trail. Numerous benches, panels with wonderful sayings, and impressive pictures invite you to take a break and concentrate on what is important during your hiking holiday in the Alps. Once a week, Margaret Gleirscher takes the guests from our hotel in Neustift in the Stubaital to the picturesque valley. Once at the top, the next highlight awaits you: a typical Tyrolean afternoon snack and convivial hours with music. Get ready to be impressed!",
                BlogImageUrl = "images/blogs/7.png",
            });

            // 8
            blogs.Add(new Blog
            {
                Title = "The family hotel in Stubaital for exciting adventures\r\nImmerse yourself in the spectacular world of the glacier\r\nHolidays at our family hotel in Stubaital – that means pure adventure. A special highlight: the ice grotto on the Stubai glacier.",
                Content = "Children love to explore the world with all their senses – especially during their holidays at our family hotel in Stubaital. A special highlight that you should not miss during your summer holidays in our family hotel is an excursion to the ice grotto on the Stubai Glacier. The valley station in Mutterberg is about 21 minutes away from the hotel. With the Eisgratbahn lift, you soar up to the ice ridge, where the adventure begins. It leads you directly to the ice grotto, where you can explore the mysterious glacier world with all your senses at the various stations. For the little guests who spend their holidays in our family hotel in the Stubaital, there is even an exciting puzzle book included. A selfie together on the icy throne is a great memory of your holiday. So, what are you waiting for?",
                BlogImageUrl = "images/blogs/8.png",
            });

            // 9
            blogs.Add(new Blog
            {
                Title = "Forest bathing in our hotel in Neustift in Stubaital\r\nDiscover the healing power of trees!",
                Content = "In Japan, forest bathing has long been regarded as a medicine. At our hotel in Neustift, you can experience up close what is behind this trend.\r\nWould you like to leave the stress and hectic pace of everyday life behind you and become one with yourself and nature again? Then our hotel in Neustift in the Stubaital has just the right thing for you: forest bathing. Our trained forest air bathing master, Werner Mayr, will take you on a very special journey of discovery as part of our varied weekly programme. This relaxation method, which we offer exclusively to the guests of the hotel, is a benefit for the body, mind and soul. The forest is a very special place of refuge for stressed people who want to slow down during their holidays in our hotel in Neustift im Stubaital and recharge their batteries. Our forest air bath master will show you a lot of exercises, which you can easily integrate into your everyday life even after your holiday here. Forest bathing does not only have a positive effect on our psyche, but also on our immune system. Try it out!",
                BlogImageUrl = "images/blogs/9.png",
            });

            await dbContext.Blogs.AddRangeAsync(blogs);
            await dbContext.SaveChangesAsync();
        }
    }
}
