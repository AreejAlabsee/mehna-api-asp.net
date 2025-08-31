// import 'package:flutter/material.dart';
// import 'api_service.dart';

// class WorkersPage extends StatefulWidget {
//   @override
//   _WorkersPageState createState() => _WorkersPageState();
// }

// class _WorkersPageState extends State<WorkersPage> {
//   List<dynamic> workers = [];
//   bool isLoading = true;
//   String errorMessage = '';

//   @override
//   void initState() {
//     super.initState();
//     _fetchWorkers();
//   }

//   Future<void> _fetchWorkers() async {
//     try {
//       final data = await ApiService.get('Users?role=worker');
//       setState(() {
//         workers = data;
//         isLoading = false;
//       });
//     } catch (e) {
//       setState(() {
//         errorMessage = 'Failed to load workers: $e';
//         isLoading = false;
//       });
//     }
//   }

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       appBar: AppBar(title: Text('قائمة العمال')),
//       body: isLoading
//           ? Center(child: CircularProgressIndicator())
//           : errorMessage.isNotEmpty
//               ? Center(child: Text(errorMessage))
//               : ListView.builder(
//                   itemCount: workers.length,
//                   itemBuilder: (context, index) {
//                     return ListTile(
//                       leading: CircleAvatar(
//                         backgroundImage: NetworkImage(
//                           workers[index]['profileImage'] ?? 'https://via.placeholder.com/150',
//                         ),
//                       ),
//                       title: Text(workers[index]['name'] ?? 'No Name'),
//                       subtitle: Text(workers[index]['specialization'] ?? 'Carpenter'),
//                       onTap: () {
//                         // الانتقال لصفحة التفاصيل
//                       },
//                     );
//                   },
//                 ),
//     );
//   }
// }
