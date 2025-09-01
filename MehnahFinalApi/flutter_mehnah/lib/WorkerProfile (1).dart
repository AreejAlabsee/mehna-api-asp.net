import 'package:flutter/material.dart';
import 'package:flutter_mehnah/models/User.dart';
import 'package:flutter_mehnah/models/Work.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class WorkerProfilePage extends StatefulWidget {
  // يجب تمرير بيانات المستخدم (العامل) إلى هذه الصفحة
  final User worker;

  const WorkerProfilePage({Key? key, required this.worker}) : super(key: key);

  @override
  State<WorkerProfilePage> createState() => _WorkerProfilePageState();
}

class _WorkerProfilePageState extends State<WorkerProfilePage> {
  late List<Work> data = [];
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    // عند تهيئة الصفحة، قم ببدء عملية جلب الأعمال الخاصة بالعامل
    fetchWorks();
  }

  Future<void> fetchWorks() async {
    try {
      // قم ببناء URL مع الـ IP الصحيح وتمرير الـ UserId
      // تم تغيير عنوان الـ IP إلى 10.0.2.2 للوصول إلى localhost من Android Emulator
      const ipAddress = '192.168.1.104';
      // تم تعديل الرابط لإضافة ?UserId=$userId كمعامل استعلام
      final url = 'http://$ipAddress:7232/api/Works';
      
      final response = await http.get(Uri.parse(url));

      // تحقق من أن الواجهة لا تزال موجودة قبل تحديث الحالة
      if (!mounted) return;

      if (response.statusCode == 200) {
        final List jsonData = json.decode(response.body);
        setState(() {
          // تأكد من أن البيانات لا تحتوي على أي قيم null قبل تحويلها
          data = jsonData.map((item) => Work.fromJson(item)).toList();
          isLoading = false;
        });
      } else {
        // إذا كان رمز الحالة ليس 200، قم بطباعة رسالة خطأ
        throw Exception('فشل في جلب البيانات: ${response.statusCode}');
      }
    } catch (e) {
      // تحقق من أن الواجهة لا تزال موجودة قبل تحديث الحالة
      if (!mounted) return;
      
      // التعامل مع أخطاء الاتصال أو التحليل
      setState(() {
        isLoading = false;
      });
      print("خطأ في جلب البيانات: $e");
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text("⚠️ خطأ في الاتصال: $e")),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Worker Profile'),
        backgroundColor: Color(0xFF5E654A),
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : SingleChildScrollView(
              child: Column(
                children: [
                  // صورة واسم العامل
                  Container(
                    padding: const EdgeInsets.all(16),
                    child: Column(
                      children: [
                        // تم إضافة فحص لـ null قبل عرض الصورة
                        widget.worker.fullProfileImageUrl != null && widget.worker.fullProfileImageUrl.isNotEmpty
                          ? CircleAvatar(
                              radius: 60,
                              backgroundImage: NetworkImage(widget.worker.fullProfileImageUrl),
                            )
                          : const CircleAvatar(
                              radius: 60,
                              child: Icon(Icons.person, size: 60),
                            ),
                        const SizedBox(height: 12),
                        Text(
                          widget.worker.Name,
                          style: const TextStyle(
                              fontSize: 22, fontWeight: FontWeight.bold),
                        ),
                      ],
                    ),
                  ),
                  const SizedBox(height: 20),
                  // بيانات التواصل
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 20),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.stretch,
                      children: [
                        const Text(
                          'Contact method ',
                          style: TextStyle(
                              fontSize: 18, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(height: 8),
                        Row(
                          children: [
                            const Icon(Icons.phone, color: Color(0xFF5E654A)),
                            const SizedBox(width: 8),
                            // استخدام رقم الهاتف من كائن العامل الممرر
                            Text(widget.worker.PhoneNumber),
                          ],
                        ),
                        const SizedBox(height: 8),
                      ],
                    ),
                  ),
                  const SizedBox(height: 20),
                  // الأعمال السابقة
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 20),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.stretch,
                      children: [
                        const Text(
                          'Previous Works',
                          style: TextStyle(
                              fontSize: 18, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(height: 8),
                        if (data.isEmpty)
                          const Text('no work .')
                        else
                          GridView.builder(
                            gridDelegate:
                                const SliverGridDelegateWithFixedCrossAxisCount(
                              crossAxisCount: 2,
                              crossAxisSpacing: 8,
                              mainAxisSpacing: 8,
                            ),
                            shrinkWrap: true,
                            physics: const NeverScrollableScrollPhysics(),
                            itemCount: data.length,
                            itemBuilder: (context, index) {
                              // تم إضافة فحص لـ null قبل عرض الصورة
                              final imageUrl = data[index].fullWorkImagesUrl;
                              return ClipRRect(
                                borderRadius: BorderRadius.circular(8),
                                child: imageUrl != null && imageUrl.isNotEmpty
                                    ? Image.network(
                                        imageUrl,
                                        fit: BoxFit.cover,
                                        errorBuilder: (context, error, stackTrace) {
                                          return const Icon(Icons.image_not_supported, size: 50);
                                        },
                                      )
                                    : const Icon(Icons.broken_image, size: 50),
                              );
                            },
                          ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
    );
  }
}
