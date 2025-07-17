

if Not Exists(Select 1 from States)
Begin
	SET IDENTITY_INSERT States ON;

	INSERT INTO States (id, CountryId, Name_ar, Name_en) 
	VALUES
	(1, 64, N'القاهرة', 'Cairo'),
	(2, 64, N'الجيزة', 'Giza'),
	(3, 64, N'الأسكندرية', 'Alexandria'),
	(4, 64, N'الدقهلية', 'Dakahlia'),
	(5, 64, N'البحر الأحمر', 'Red Sea'),
	(6, 64, N'البحيرة', 'Beheira'),
	(7, 64, N'الفيوم', 'Fayoum'),
	(8, 64, N'الغربية', 'Gharbiya'),
	(9, 64, N'الإسماعلية', 'Ismailia'),
	(10,64, N'المنوفية', 'Menofia'),
	(11,64, N'المنيا', 'Minya'),
	(12,64, N'القليوبية', 'Qaliubiya'),
	(13,64, N'الوادي الجديد', 'New Valley'),
	(14,64, N'السويس', 'Suez'),
	(15,64, N'اسوان', 'Aswan'),
	(16,64, N'اسيوط', 'Assiut'),
	(17,64, N'بني سويف', 'Beni Suef'),
	(18,64, N'بورسعيد', 'Port Said'),
	(19,64, N'دمياط', 'Damietta'),
	(20,64, N'الشرقية', 'Sharkia'),
	(21,64, N'جنوب سيناء', 'South Sinai'),
	(22,64, N'كفر الشيخ', 'Kafr Al sheikh'),
	(23,64, N'مطروح', 'Matrouh'),
	(24,64, N'الأقصر', 'Luxor'),
	(25,64, N'قنا', 'Qena'),
	(26,64, N'شمال سيناء', 'North Sinai'),
	(27,64, N'سوهاج', 'Sohag');

	SET IDENTITY_INSERT States OFF;
End