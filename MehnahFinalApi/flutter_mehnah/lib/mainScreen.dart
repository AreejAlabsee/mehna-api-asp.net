// import 'dart:convert';
// import 'dart:core';

// import 'package:flutter/material.dart';
// import 'package:flutter_mehnah/ClintSignPage%20(1).dart';
// import 'package:flutter_mehnah/models/User.dart';
// import 'package:http/http.dart' as http;

// class mainScreen extends StatefulWidget {
//   const mainScreen({super.key});

//   @override
//   State<mainScreen> createState() => _mainScreenState();
// }

// class _mainScreenState extends State<mainScreen> {
//   late Future<List<User>> usersFuture;

//   @override
//   void initState() {
//     super.initState();
//     usersFuture = getUsers();
//   }

//   Future<List<User>> getUsers() async {
//     const url = 'http://192.168.1.104:7232/api/Users';
//     try {
//       final response = await http.get(Uri.parse(url));

//       if (response.statusCode == 200) {
//         final body = json.decode(response.body) as List;
//         return body.map<User>(User.fromJson).toList();
//       } else {
//         throw Exception('Failed to load users: ${response.statusCode}');
//       }
//     } catch (e) {
//       throw Exception('Failed to connect to the server: $e');
//     }
//   }

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//      appBar: AppBar(
//         backgroundColor: const Color(0xFF596037),
//         leading: IconButton(
//           icon: const Icon(Icons.arrow_back, color: Colors.white),
//           onPressed: () {Navigator.pop(
//                 context,
//                 MaterialPageRoute(builder: (context) => ClintSignPage ()),
//               );},
//         ),
//         title: const Text(
//           'Worker',
//           style: TextStyle(color: Colors.white),
//         ),
//         centerTitle: true,
//         actions: [
//           IconButton(
//             icon: const Icon(Icons.more_vert, color: Colors.white),
//             onPressed: () {},
//           ),
//         ],
//         elevation: 0,
//       ),
    
//       body: Center(
          
//         child: FutureBuilder<List<User>>(
//           future: usersFuture,
//           builder: (context, snapshot) {
//             if (snapshot.connectionState == ConnectionState.waiting) {
//               return const CircularProgressIndicator();
//             } else if (snapshot.hasError) {
//               return Text('Error: ${snapshot.error}');
//             } else if (snapshot.hasData) {
//               return buildUsers(snapshot.data!);
//             } else {
//               return const Text('No users found.');
//             }
//           },
//         ),
//       ),
//     );
//   }

//   Widget buildUsers(List<User> users) {
//     return ListView.builder(
//       itemCount: users.length,
//       itemBuilder: (context, index) {
//         final user = users[index];
//         return Card(
//           child: ListTile(
//             leading: CircleAvatar(
//               radius: 28,
//               backgroundImage: NetworkImage(user.fullProfileImageUrl),

//             ),
//             title: Text(user.Name),
//             subtitle: Text(user.PhoneNumber),
//           ),
//         );
//       },
//     );
//   }
// }

// main_screen.dart

import 'dart:convert';
import 'dart:core';
import 'package:flutter/material.dart';
import 'package:flutter_mehnah/WorkerProfile%20(1).dart';
import 'package:http/http.dart' as http;
import 'package:flutter_mehnah/ClintSignPage%20(1).dart'; // تأكد من المسار
import 'package:flutter_mehnah/models/User.dart'; //// جديد: استيراد صفحة الملف الشخصي

class mainScreen extends StatefulWidget {
  const mainScreen({super.key});

  @override
  State<mainScreen> createState() => _mainScreenState();
}

class _mainScreenState extends State<mainScreen> {
  late Future<List<User>> usersFuture;

  @override
  void initState() {
    super.initState();
    usersFuture = getUsers();
  }

  Future<List<User>> getUsers() async {
    const url = 'http://192.168.1.104:7232/api/Users';
    try {
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        final body = json.decode(response.body) as List;
        return body.map<User>(User.fromJson).toList();
      } else {
        throw Exception('Failed to load users: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Failed to connect to the server: $e');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color(0xFF596037),
        leading: IconButton(
          icon: const Icon(Icons.arrow_back, color: Colors.white),
          onPressed: () {
            Navigator.pop(
              context,
              MaterialPageRoute(builder: (context) => const SignPage()),
            );
          },
        ),
        title: const Text(
          'Worker',
          style: TextStyle(color: Colors.white),
        ),
        centerTitle: true,
        actions: [
          IconButton(
            icon: const Icon(Icons.more_vert, color: Colors.white),
            onPressed: () {},
          ),
        ],
        elevation: 0,
      ),
      body: Center(
        child: FutureBuilder<List<User>>(
          future: usersFuture,
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.waiting) {
              return const CircularProgressIndicator();
            } else if (snapshot.hasError) {
              return Text('Error: ${snapshot.error}');
            } else if (snapshot.hasData) {
              return buildUsers(snapshot.data!);
            } else {
              return const Text('No users found.');
            }
          },
        ),
      ),
    );
  }

  Widget buildUsers(List<User> users) {
    return ListView.builder(
      itemCount: users.length,
      itemBuilder: (context, index) {
        final user = users[index];
        return InkWell( // جديد: لجعل البطاقة قابلة للنقر
          onTap: () {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) => WorkerProfilePage(worker: user), // جديد: تمرير كائن المستخدم إلى الصفحة الجديدة
              ),
            );
          },
          child: Card(
            child: ListTile(
              leading: CircleAvatar(
                radius: 28,
                backgroundImage: NetworkImage(user.fullProfileImageUrl),
              ),
              title: Text(user.Name),
              subtitle: Row(children: [Text(user.PhoneNumber )],)
             
              
            ),
          ),
        );
      },
    );
  }
}