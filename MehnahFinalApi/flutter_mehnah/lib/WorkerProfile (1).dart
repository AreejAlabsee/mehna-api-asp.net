// import 'package:flutter/material.dart';
// import 'package:flutter_application_16/Work%20(1).dart';
// import 'package:flutter_application_16/api_service%20(1).dart';
// import 'package:http/http.dart' as http;
// import 'dart:convert';


// class WorkerProfilePage extends StatefulWidget {
//   const WorkerProfilePage({super.key});

//   @override
//   State<WorkerProfilePage> createState() => _WorkerProfilePageState();
// }

// class _WorkerProfilePageState extends State<WorkerProfilePage> {
// ApiService apiService = ApiService();
//   late List<Work> data = [];
//   void getData()async{
//   // data =await apiService.getWorks();}
//   bool isLoading = true;

//   @override
//   void initState() {
//     super.initState();
//     fetchWorks();
//   }

//   Future<void> fetchWorks() async {
//     try {
//       final response = await http.get(
//         Uri.parse('https://localhost:7232/api/Works?UserId=3'), // ضع رابط API
//       );

//       if (response.statusCode == 200) {
//         List jsonData = json.decode(response.body);
//         setState(() {
//           data = jsonData.map((item) => Work.fromJson(item)).toList();
//           isLoading = false;
//         });
//       } else {
//         throw Exception('فشل في جلب البيانات');
//       }
//     } catch (e) {
//       setState(() {
//         isLoading = false;
//       });
//       print("خطأ في جلب البيانات: $e");
//     }
//   }

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       appBar: AppBar(
//         title: const Text('ملف العامل'),
//         backgroundColor: Color(0xFF5E654A),
//       ),
//       body: isLoading
//           ? const Center(child: CircularProgressIndicator())
//           : SingleChildScrollView(
//               child: Column(
//                 children: [
//                   // صورة واسم العامل
//                   Container(
//                     padding: const EdgeInsets.all(16),
//                     child: Column(
//                       children: [
//                         const CircleAvatar(
//                           radius: 60,
//                           backgroundImage: AssetImage(''),
//                         ),
//                         const SizedBox(height: 12),
//                         Text('$data[index].Name',
//                           style: const TextStyle(
//                               fontSize: 22, fontWeight: FontWeight.bold),
//                         ),
                     
//                       ],
//                     ),
//                   ),

//                   // بيانات التواصل
//                   Padding(
//                     padding: const EdgeInsets.symmetric(horizontal: 20),
//                     child: Column(
//                       crossAxisAlignment: CrossAxisAlignment.stretch,
//                       children: [
//                         const Text(
//                           'بيانات التواصل',
//                           style: TextStyle(
//                               fontSize: 18, fontWeight: FontWeight.bold),
//                         ),
//                         const SizedBox(height: 8),
//                         Row(
//                           children: const [
//                             Icon(Icons.phone, color: Color(0xFF5E654A)),
//                             SizedBox(width: 8),
//                             Text('+967 777 777 777'),
//                           ],
//                         ),
//                         const SizedBox(height: 8),
                       
                        
//                       ],
//                     ),
//                   ),

//                   const SizedBox(height: 20),

//                   // الأعمال السابقة
//                   Padding(
//                     padding: const EdgeInsets.symmetric(horizontal: 20),
//                     child: Column(
//                       crossAxisAlignment: CrossAxisAlignment.stretch,
//                       children: [
//                         const Text(
//                           'الأعمال السابقة',
//                           style: TextStyle(
//                               fontSize: 18, fontWeight: FontWeight.bold),
//                         ),
//                         const SizedBox(height: 8),
//                         GridView.builder(
//                           gridDelegate:
//                               const SliverGridDelegateWithFixedCrossAxisCount(
//                             crossAxisCount: 2,
//                             crossAxisSpacing: 8,
//                             mainAxisSpacing: 8,
//                           ),
//                           shrinkWrap: true,
//                           physics: const NeverScrollableScrollPhysics(),
//                           itemCount: data.length,
//                           itemBuilder: (context, index) {
//                             return ClipRRect(
//                               borderRadius: BorderRadius.circular(8),
//                               child: Image.network(
//                                 data[index].WorkImagesUrl, // رابط الصورة من API
//                                 fit: BoxFit.cover,
//                               ),
//                             );
//                           },
//                         ),
//                       ],
//                     ),
//                   ),
//                 ],
//               ),
//             ),
//     );
//   }
// }
// }


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
    fetchWorks(widget.worker.id);
  }

  Future<void> fetchWorks(int userId) async {
    try {
      // قم ببناء URL مع الـ IP الصحيح وتمرير الـ UserId
      const ipAddress = '192.168.1.104'; // قم بتغييره إلى IP الخاص بك
      final url = 'http://$ipAddress:7232/api/Works?UserId=$userId';
      
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        final List jsonData = json.decode(response.body);
        setState(() {
          data = jsonData.map((item) => Work.fromJson(item)).toList();
          isLoading = false;
        });
      } else {
        throw Exception('فشل في جلب البيانات: ${response.statusCode}');
      }
    } catch (e) {
      setState(() {
        isLoading = false;
      });
      print("خطأ في جلب البيانات: $e");
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('ملف العامل'),
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
                        CircleAvatar(
                          radius: 60,
                          // استخدام صورة العامل من كائن الـ User الممرر
                          backgroundImage: NetworkImage(widget.worker.fullProfileImageUrl),
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

                  // الأعمال السابقة
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 20),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.stretch,
                      children: [
                        const Text(
                          'الأعمال السابقة',
                          style: TextStyle(
                              fontSize: 18, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(height: 8),
                        if (data.isEmpty)
                          const Text('لا يوجد أعمال سابقة لعرضها.')
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
                              return ClipRRect(
                                borderRadius: BorderRadius.circular(8),
                                child: Image.network(
                                  data[index].fullWorkImagesUrl, // استخدم getter للحصول على الرابط الكامل
                                  fit: BoxFit.cover,
                                ),
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