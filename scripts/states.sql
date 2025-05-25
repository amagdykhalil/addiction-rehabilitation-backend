

if Not Exists(Select 1 from States)
Begin
	SET IDENTITY_INSERT States ON;

	INSERT INTO States (id, CountryId, Name_ar, Name_en) 
	VALUES
	(1, 64,'القاهرة', 'Cairo'),
	(2, 64,'الجيزة', 'Giza'),
	(3, 64,'الأسكندرية', 'Alexandria'),
	(4, 64,'الدقهلية', 'Dakahlia'),
	(5, 64,'البحر الأحمر', 'Red Sea'),
	(6, 64,'البحيرة', 'Beheira'),
	(7, 64,'الفيوم', 'Fayoum'),
	(8, 64,'الغربية', 'Gharbiya'),
	(9, 64,'الإسماعلية', 'Ismailia'),
	(10,64, 'المنوفية', 'Menofia'),
	(11,64, 'المنيا', 'Minya'),
	(12,64, 'القليوبية', 'Qaliubiya'),
	(13,64, 'الوادي الجديد', 'New Valley'),
	(14,64, 'السويس', 'Suez'),
	(15,64, 'اسوان', 'Aswan'),
	(16,64, 'اسيوط', 'Assiut'),
	(17,64, 'بني سويف', 'Beni Suef'),
	(18,64, 'بورسعيد', 'Port Said'),
	(19,64, 'دمياط', 'Damietta'),
	(20,64, 'الشرقية', 'Sharkia'),
	(21,64, 'جنوب سيناء', 'South Sinai'),
	(22,64, 'كفر الشيخ', 'Kafr Al sheikh'),
	(23,64, 'مطروح', 'Matrouh'),
	(24,64, 'الأقصر', 'Luxor'),
	(25,64, 'قنا', 'Qena'),
	(26,64, 'شمال سيناء', 'North Sinai'),
	(27,64, 'سوهاج', 'Sohag');

	SET IDENTITY_INSERT States OFF;
End