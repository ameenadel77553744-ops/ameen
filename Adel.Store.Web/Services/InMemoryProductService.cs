using Adel.Store.Web.Models;
using System.Globalization;

namespace Adel.Store.Web.Services
{
    public class InMemoryProductService : IProductService
    {
        private static readonly Random Randomizer = new();
        private static readonly List<Category> Categories = new()
        {
            new Category { Id = 1,  NameAr = "الأزياء الرسمية",    NameEn = "Formal Wear" },
            new Category { Id = 2,  NameAr = "ملابس الأطفال",     NameEn = "Kids Fashion" },
            new Category { Id = 3,  NameAr = "الألعاب والترفيه",           NameEn = "Toys & Entertainment" },
            new Category { Id = 4,  NameAr = "الأزياء الرجالية",   NameEn = "Men's Fashion" },
            new Category { Id = 5,  NameAr = "الأزياء النسائية",   NameEn = "Women's Fashion" },
            new Category { Id = 6,  NameAr = "التقنية والإلكترونيات",     NameEn = "Technology & Electronics" },
            new Category { Id = 7,  NameAr = "المنزل والديكور",     NameEn = "Home & Decor" },
            new Category { Id = 8,  NameAr = "العناية والجمال",    NameEn = "Beauty & Wellness" },
            new Category { Id = 9,  NameAr = "الرياضة واللياقة",           NameEn = "Sports & Fitness" },
            new Category { Id = 10, NameAr = "الإكسسوارات الفاخرة",       NameEn = "Luxury Accessories" },
        };

        private static readonly List<Product> Products = GenerateProducts();

        private static List<Product> GenerateProducts()
        {
            var categoryImages = new Dictionary<int, string[]>
            {
                { 1,  new [] { 
                    "https://images.unsplash.com/photo-1520637836862-4d197d17c35a?w=1200&q=80", // بدلة رسمية رجالية
                    "https://images.unsplash.com/photo-1594633312681-425c7b97ccd1?w=1200&q=80", // فستان رسمي نسائي
                    "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=1200&q=80", // حقيبة يد رسمية
                    "https://images.unsplash.com/photo-1524592094714-0f0654e20314?w=1200&q=80", // ساعة فاخرة
                    "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=1200&q=80"  // حذاء رسمي
                }},
                { 2,  new [] { 
                    "https://images.unsplash.com/photo-1503919005314-30d93d07d823?w=1200&q=80", // ملابس أطفال عصرية
                    "https://images.unsplash.com/photo-1519238263530-99bdd11df2ea?w=1200&q=80", // تيشيرت أطفال ملون
                    "https://images.unsplash.com/photo-1518831959646-742c3a14ebf7?w=1200&q=80", // فستان أطفال جميل
                    "https://images.unsplash.com/photo-1578662996442-48f60103fc96?w=1200&q=80", // جاكيت أطفال
                    "https://images.unsplash.com/photo-1560472354-b33ff0c44a43?w=1200&q=80"  // أحذية أطفال رياضية
                }},
                { 3,  new [] { 
                    "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=1200&q=80", // لعبة تعليمية حديثة
                    "https://images.unsplash.com/photo-1606092195730-5d7b9af1efc5?w=1200&q=80", // أحجية تعليمية
                    "https://images.unsplash.com/photo-1566133894768-6d0d5dce8c8b?w=1200&q=80", // لعبة تركيب
                    "https://images.unsplash.com/photo-1606107557195-0e29a4b5b4aa?w=1200&q=80", // لعبة جماعية
                    "https://images.unsplash.com/photo-1520637836862-4d197d17c35a?w=1200&q=80"  // لعبة إلكترونية
                }},
                { 4,  new [] { 
                    "https://images.unsplash.com/photo-1596755094514-f87e34085b2c?w=1200&q=80", // قميص رجالي أنيق
                    "https://images.unsplash.com/photo-1542272604-787c3835535d?w=1200&q=80", // جينز رجالي
                    "https://images.unsplash.com/photo-1551698618-1dfe5d97d256?w=1200&q=80", // سترة رجالية
                    "https://images.unsplash.com/photo-1549298916-b41d501d3772?w=1200&q=80", // حذاء رياضي رجالي
                    "https://images.unsplash.com/photo-1524592094714-0f0654e20314?w=1200&q=80"  // ساعة رجالية
                }},
                { 5,  new [] { 
                    "https://images.unsplash.com/photo-1515372039744-b8f02a3ae446?w=1200&q=80", // فستان نسائي أنيق
                    "https://images.unsplash.com/photo-1594633312681-425c7b97ccd1?w=1200&q=80", // بلوزة نسائية
                    "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=1200&q=80", // كعب نسائي
                    "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=1200&q=80", // حقيبة نسائية
                    "https://images.unsplash.com/photo-1583496661160-fb5886a13d24?w=1200&q=80"  // تنورة
                }},
                { 6,  new [] { 
                    "https://images.unsplash.com/photo-1592750475338-74b7b21085ab?w=1200&q=80", // هاتف ذكي حديث
                    "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=1200&q=80", // حاسوب محمول
                    "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=1200&q=80", // سماعات عالية الجودة
                    "https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=1200&q=80", // كاميرا احترافية
                    "https://images.unsplash.com/photo-1434493789847-2f02dc6ca35d?w=1200&q=80"  // ساعة ذكية
                }},
                { 7,  new [] { 
                    "https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=1200&q=80", // طقم قدور فاخر
                    "https://images.unsplash.com/photo-1593618998160-e34014e67546?w=1200&q=80", // طقم سكاكين
                    "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=1200&q=80", // مكنسة كهربائية
                    "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=1200&q=80", // مصباح أنيق
                    "https://images.unsplash.com/photo-1586023492125-27b2c045efd7?w=1200&q=80"  // سجادة فاخرة
                }},
                { 8,  new [] { 
                    "https://images.unsplash.com/photo-1541643600914-78b084683601?w=1200&q=80", // عطر فاخر
                    "https://images.unsplash.com/photo-1556228720-195a672e8a03?w=1200&q=80", // لوشن للجسم
                    "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=1200&q=80", // شامبو طبيعي
                    "https://images.unsplash.com/photo-1570194065650-d99fb4bedf0a?w=1200&q=80", // كريم وجه
                    "https://images.unsplash.com/photo-1586495777744-4413f21062fa?w=1200&q=80"  // أحمر شفاه
                }},
                { 9,  new [] { 
                    "https://images.unsplash.com/photo-1551698618-1dfe5d97d256?w=1200&q=80", // كرة قدم احترافية
                    "https://images.unsplash.com/photo-1546519638-68e109498ffc?w=1200&q=80", // كرة سلة
                    "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?w=1200&q=80", // مضرب تنس
                    "https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?w=1200&q=80", // حصيرة يوغا
                    "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=1200&q=80"  // حذاء جري
                }},
                { 10, new [] { 
                    "https://images.unsplash.com/photo-1572635196237-14b3f281503f?w=1200&q=80", // نظارات شمسية عصرية
                    "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=1200&q=80", // محفظة جلدية
                    "https://images.unsplash.com/photo-1515377905703-c4788e51af15?w=1200&q=80", // عقد ذهبي
                    "https://images.unsplash.com/photo-1611652022419-a9419f74343d?w=1200&q=80", // سوار أنيق
                    "https://images.unsplash.com/photo-1535632066927-ab7c9ab60908?w=1200&q=80"  // أقراط فضية
                }},
            };

            var namesByCategory = new Dictionary<int, (string en, string ar)[]>
            {
                { 1,  new [] { ("Premium Business Suit", "بدلة عمل فاخرة"), ("Elegant Evening Dress", "فستان سهرة أنيق"), ("Luxury Leather Bag", "حقيبة جلد فاخرة"), ("Swiss Watch", "ساعة سويسرية"), ("Italian Leather Shoes", "حذاء جلد إيطالي"), ("Silk Tie", "ربطة عنق حريرية"), ("Designer Blazer", "جاكيت مصمم"), ("Formal Shirt", "قميص رسمي"), ("Pearl Necklace", "عقد لؤلؤ"), ("Gold Cufflinks", "أزرار أكمام ذهبية") } },
                { 2,  new [] { ("Premium Kids Outfit", "طقم أطفال فاخر"), ("Colorful Kids T-Shirt", "تيشيرت أطفال ملون"), ("Princess Dress", "فستان أميرة"), ("Kids Winter Jacket", "جاكيت شتوي للأطفال"), ("Sports Shoes for Kids", "حذاء رياضي للأطفال"), ("School Backpack", "حقيبة مدرسية"), ("Kids Pajama Set", "طقم بيجامة أطفال"), ("Summer Shorts", "شورت صيفي"), ("Kids Cap", "قبعة أطفال"), ("Cartoon Hoodie", "هودي كرتوني") } },
                { 3,  new [] { ("Educational Toy", "لعبة تعليمية"), ("3D Puzzle", "أحجية ثلاثية الأبعاد"), ("Family Board Game", "لعبة عائلية"), ("Building Blocks", "مكعبات البناء"), ("Electronic Game", "لعبة إلكترونية"), ("Creative Art Set", "طقم فنون إبداعية"), ("Science Kit", "طقم علوم"), ("Musical Instrument", "آلة موسيقية"), ("Remote Control Car", "سيارة تحكم عن بعد"), ("Interactive Robot", "روبوت تفاعلي") } },
                { 4,  new [] { ("Designer Men's Shirt", "قميص رجالي مصمم"), ("Premium Denim Jeans", "جينز دنيم فاخر"), ("Leather Jacket", "جاكيت جلد"), ("Luxury Sneakers", "حذاء رياضي فاخر"), ("Swiss Chronograph", "ساعة كرونوغراف سويسرية"), ("Italian Leather Belt", "حزام جلد إيطالي"), ("Cashmere Hoodie", "هودي كشمير"), ("Tailored Suit", "بدلة مفصلة"), ("Polo Shirt", "قميص بولو"), ("Chino Shorts", "شورت تشينو") } },
                { 5,  new [] { ("Elegant Evening Gown", "فستان سهرة أنيق"), ("Silk Blouse", "بلوزة حريرية"), ("Designer High Heels", "كعب عالي مصمم"), ("Luxury Handbag", "حقيبة يد فاخرة"), ("Pleated Skirt", "تنورة مطوية"), ("Cashmere Scarf", "وشاح كشمير"), ("Ankle Boots", "جزمة كاحل"), ("Strappy Sandals", "صندل بأحزمة"), ("Blazer Jacket", "جاكيت بليزر"), ("Skinny Jeans", "جينز ضيق") } },
                { 6,  new [] { ("iPhone 15 Pro Max", "آيفون 15 برو ماكس"), ("MacBook Pro M3", "ماك بوك برو M3"), ("AirPods Pro", "إير بودز برو"), ("Canon EOS R6", "كانون EOS R6"), ("Apple Watch Ultra", "أبل واتش ألترا"), ("iPad Pro 12.9", "آيباد برو 12.9"), ("Magic Keyboard", "لوحة مفاتيح ماجيك"), ("Logitech MX Master", "لوجيتك MX ماستر"), ("Samsung 4K Monitor", "شاشة سامسونج 4K"), ("Bose Speaker", "مكبر صوت بوز") } },
                { 7,  new [] { ("Premium Cookware Set", "طقم قدور فاخر"), ("German Steel Knives", "سكاكين فولاذ ألماني"), ("Robot Vacuum", "مكنسة روبوت"), ("Designer Table Lamp", "مصباح طاولة مصمم"), ("Persian Rug", "سجادة فارسية"), ("Velvet Cushions", "وسائد مخمل"), ("Egyptian Cotton Sheets", "مفارش قطن مصري"), ("Blackout Curtains", "ستائر معتمة"), ("Digital Air Fryer", "قلاية هوائية رقمية"), ("Espresso Machine", "ماكينة إسبريسو") } },
                { 8,  new [] { ("Chanel No.5 Perfume", "عطر شانيل رقم 5"), ("Luxury Body Lotion", "لوشن جسم فاخر"), ("Organic Shampoo", "شامبو عضوي"), ("Keratin Conditioner", "بلسم كيراتين"), ("Anti-Aging Cream", "كريم مضاد للشيخوخة"), ("Matte Lipstick", "أحمر شفاه مطفي"), ("Waterproof Mascara", "ماسكارا مقاومة للماء"), ("Vitamin C Serum", "سيروم فيتامين سي"), ("SPF 50 Sunscreen", "واقي شمس SPF 50"), ("Moisturizing Hand Cream", "كريم يدين مرطب") } },
                { 9,  new [] { ("FIFA Official Football", "كرة قدم فيفا رسمية"), ("NBA Basketball", "كرة سلة NBA"), ("Wilson Tennis Racket", "مضرب تنس ويلسون"), ("Premium Yoga Mat", "حصيرة يوغا فاخرة"), ("Nike Running Shoes", "حذاء جري نايك"), ("Safety Cycling Helmet", "خوذة دراجة آمنة"), ("Adjustable Dumbbells", "دمبل قابل للتعديل"), ("Speed Jump Rope", "حبل قفز سريع"), ("Professional Swim Goggles", "نظارات سباحة احترافية"), ("Smart Fitness Watch", "ساعة لياقة ذكية") } },
                { 10, new [] { ("Ray-Ban Sunglasses", "نظارات راي بان"), ("Louis Vuitton Wallet", "محفظة لويس فيتون"), ("Diamond Necklace", "عقد ماس"), ("Gold Bracelet", "سوار ذهبي"), ("Pearl Earrings", "أقراط لؤلؤ"), ("Designer Cap", "قبعة مصممة"), ("Gucci Belt", "حزام غوتشي"), ("Luxury Keychain", "سلسلة مفاتيح فاخرة"), ("Platinum Ring", "خاتم بلاتين"), ("Hermès Scarf", "وشاح هيرميس") } },
            };

            var list = new List<Product>();
            int id = 1;
            foreach (var cat in Categories)
            {
                var names = namesByCategory[cat.Id];
                var imgs = categoryImages.ContainsKey(cat.Id) ? categoryImages[cat.Id] : Array.Empty<string>();
                for (int i = 0; i < names.Length; i++)
                {
                    var (en, ar) = names[i];
                    var img = imgs.Length > 0 ? imgs[i % imgs.Length] + $"&cat={cat.Id}&i={i}&t={DateTime.Now.Ticks}" : "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=1200&q=80";
                    list.Add(new Product
                    {
                        Id = id++,
                        NameEn = en,
                        NameAr = ar,
                        DescriptionEn = "Authentic premium product crafted for the Saudi market. Experience luxury shopping with fast delivery across the Kingdom. Perfect for the modern Saudi lifestyle.",
                        DescriptionAr = "منتج أصلي فاخر مصمم خصيصاً للسوق السعودي. استمتع بتجربة تسوق راقية مع توصيل سريع في جميع أنحاء المملكة. مثالي لنمط الحياة السعودي العصري.",
                        ImageUrl = img,
                        Price = new Money(Randomizer.Next(50, 2001), Currency.SAR),
                        CategoryId = cat.Id,
                        IsActive = true,
                        IsFeatured = Randomizer.NextDouble() > 0.7
                    });
                }
            }
            return list;
        }

        public List<Category> GetCategories() => Categories;

        public List<Product> GetProducts(int? categoryId, bool? featured, string? query, int page, int pageSize, out int totalCount)
        {
            // Update random prices each call for demo
            foreach (var p in Products)
            {
                p.Price = new Money(Randomizer.Next(50, 2001), Currency.SAR);
            }

            var items = Products.Where(p => p.IsActive).AsEnumerable();
            if (categoryId.HasValue) items = items.Where(p => p.CategoryId == categoryId.Value);
            if (featured == true) items = items.Where(p => p.IsFeatured);
            if (!string.IsNullOrWhiteSpace(query))
            {
                var term = query.Trim();
                items = items.Where(p => p.NameAr.Contains(term, StringComparison.OrdinalIgnoreCase)
                                       || p.NameEn.Contains(term, StringComparison.OrdinalIgnoreCase)
                                       || p.DescriptionAr.Contains(term, StringComparison.OrdinalIgnoreCase)
                                       || p.DescriptionEn.Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            totalCount = items.Count();
            return items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public Product? GetById(int id) => Products.FirstOrDefault(p => p.Id == id);

        public void ToggleActive(int id)
        {
            var p = Products.FirstOrDefault(x => x.Id == id);
            if (p != null) p.IsActive = !p.IsActive;
        }

        public void ToggleFeatured(int id)
        {
            var p = Products.FirstOrDefault(x => x.Id == id);
            if (p != null) p.IsFeatured = !p.IsFeatured;
        }
    }
}