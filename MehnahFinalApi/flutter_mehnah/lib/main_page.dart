
// // import 'package:flutter/material.dart';
// // import 'package:flutter_mehnah/models/ApiHandler.dart';
// // import 'package:flutter_mehnah/models/User.dart';

// // class MainPage extends StatefulWidget {
// //   const MainPage({super.key});

 
// //   State<MainPage> createState() => _MainPageState();
// // }

// // class _MainPageState extends State<MainPage> {
// // Apihandler apihandler =Apihandler();
// // late List<User> data =[];
// // void getData() async{
// //   data = await apihandler.getUserData();
// //   setState(() {
    
// //   });
// // }
// //  @override
// // void initState(){
// //   super.initState();
// //   getData();
  
// // }
// //   @override
// //   Widget build(BuildContext context) {
// //     return Scaffold(
// //       appBar: AppBar(backgroundColor: Colors.amber,
// //       title: Text("Users"),),
// //       bottomNavigationBar: MaterialButton(onPressed: getData),
// //       body: Column(children: [

// //         ListView.builder(itemCount:data.length ,
// //         shrinkWrap: true,
// //         itemBuilder:(BuildContext context ,int index){
// //           return ListTile(
// //             leading: Text("${data[index].Id}"),
// //             title: Text(data[index].Name),
// //             subtitle:Text(data[index].PhoneNumber) ,
// //           );
// //         } ,)
// //       ],),
// //     );
// //   }
// // }
// import 'package:flutter/material.dart';
// import 'package:flutter_mehnah/models/ApiHandler.dart';
// import 'package:flutter_mehnah/models/User.dart';

// class MainPage extends StatefulWidget {
//   const MainPage({super.key});

//   @override
//   State<MainPage> createState() => _MainPageState();
// }

// class _MainPageState extends State<MainPage> {
//   final Apihandler apihandler = Apihandler();
//   List<User> _data = [];
//   bool _isLoading = false;
//   String? _errorMessage;

//   @override
//   void initState() {
//     super.initState();
//     _fetchData();
//   }

//   void _fetchData() async {
//     // Start by showing the loading indicator
//     setState(() {
//       _isLoading = true;
//       _errorMessage = null; // Clear any previous errors
//     });

//     // Await the data from the API handler
//     final fetchedData = await apihandler.getUserData();

//     // Update the state based on the fetched data
//     setState(() {
//       if (fetchedData.isEmpty) {
//         _errorMessage = 'Failed to fetch data or no users found.';
//       } else {
//         _data = fetchedData;
//       }
//       _isLoading = false; // Hide the loading indicator
//     });
//   }

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       appBar: AppBar(
//         backgroundColor: Colors.amber,
//         title: const Text("Users"),
//       ),
//       bottomNavigationBar: MaterialButton(
//         onPressed: _fetchData,
//         child: const Text('Refresh'),
//       ),
//       body: Center(
//         child: _isLoading
//             ? const CircularProgressIndicator()
//             : _errorMessage != null
//                 ? Padding(
//                     padding: const EdgeInsets.all(16.0),
//                     child: Text(
//                       _errorMessage!,
//                       style: const TextStyle(color: Colors.red, fontSize: 16),
//                       textAlign: TextAlign.center,
//                     ),
//                   )
//                 : _data.isEmpty
//                     ? const Text('No users to display.')
//                     : Expanded(
//                         child: ListView.builder(
//                           itemCount: _data.length,
//                           itemBuilder: (BuildContext context, int index) {
//                             return ListTile(
//                               leading: Text("${_data[index].Id}"),
//                               title: Text(_data[index].Name),
//                               subtitle: Text(_data[index].PhoneNumber),
//                             );
//                           },
//                         ),
//                       ),
//       ),
//     );
//   }
// // }
import 'package:flutter/material.dart';
import 'package:flutter_mehnah/models/ApiHandler.dart';
import 'package:flutter_mehnah/models/User.dart';

class MainPage extends StatefulWidget {
  const MainPage({super.key});

  @override
  State<MainPage> createState() => _MainPageState();
}

class _MainPageState extends State<MainPage> {
  final Apihandler apihandler = Apihandler();
  List<User> data = [];
  bool _isLoading = false;
  String? _errorMessage;

  @override
  void initState() {
    super.initState();
    _fetchData();
  }

  void _fetchData() async {
    // Start by showing the loading indicator
    setState(() {
      _isLoading = true;
      _errorMessage = null; // Clear any previous errors
    });

    try {
      // Await the data from the API handler
      final fetchedData = await apihandler.getUserData();
      // Update the state based on the fetched data
      setState(() {
        if (fetchedData.isEmpty) {
          _errorMessage = 'Failed to fetch data or no users found.';
        } else {
          data = fetchedData;
        }
        _isLoading = false; // Hide the loading indicator
      });
    } catch (e) {
      // Catch the exception and set the error message
      setState(() {
        _isLoading = false;
        _errorMessage = e.toString().replaceFirst('Exception: ', '');
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.amber,
        title: const Text("Users"),
      ),
      bottomNavigationBar: MaterialButton(
        onPressed: _fetchData,
        child: const Text('Refresh'),
      ),
      body: Center(
        child: _isLoading
            ? const CircularProgressIndicator()
            : _errorMessage != null
                ? Padding(
                    padding: const EdgeInsets.all(16.0),
                    child: Text(
                      _errorMessage!,
                      style: const TextStyle(color: Colors.red, fontSize: 16),
                      textAlign: TextAlign.center,
                    ),
                  )
                : data.isEmpty
                    ? const Text('No users to display.')
                    : Expanded(
                        child: ListView.builder(
                          itemCount: data.length,
                          itemBuilder: (BuildContext context, int index) {
                            return ListTile(
                              leading: Text("${data[index].id}"),
                              title: Text(data[index].Name),
                              subtitle: Text(data[index].PhoneNumber),
                            );
                          },
                        ),
                      ),
      ),
    );
  }
}

// import 'dart:io';

// import 'package:dio/dio.dart';
// import 'package:flutter/material.dart';


// String url = Platform.isAndroid ? 'http://192.168.1.1:5254' : 'http://localhost:5254';
// class Postmeth extends StatefulWidget {
//   const Postmeth({super.key});

//   @override
//   State<Postmeth> createState() => _PostmethState();
// }

// class _PostmethState extends State<Postmeth> {
//   List posts=[];

//   @override
//   void initState(){
//     super.initState();
//     featchdata();
//   }
//   void featchdata()async{
//     try {
//       var res=await Dio().get('$url/api/Users'); 
//       setState(() {
//         posts =res.data;
//       });
//     }
//     catch(e){
// print('Error $e');
//     }
//   }

//   Widget build(BuildContext context) {
//     return Scaffold(
//             appBar: AppBar(
//         backgroundColor: Colors.amber,
//         title: const Text("Users"),
//       ),
//       body: ListView.builder(itemCount: posts.length,
//         itemBuilder:(context, index) {
// return ListTile(
//    leading: Text("${posts[index].Id}"),
//     title: Text(posts[index].Name),
//     subtitle: Text(posts[index].PhoneNumber),
                           
// );
//       }),
//     );
//   }
// }