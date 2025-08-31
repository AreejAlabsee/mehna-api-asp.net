// import 'dart:convert';

// import 'package:flutter_mehnah/models/User.dart';
// import 'package:http/http.dart' as http;

// class Apihandler {
//   final String baseUri = "http://localhost:5254/api/Users";


// Future<List<User>> getUserData() async{
//   List<User> data = [];
//   final uri =Uri.parse(baseUri);
//   try{
// final response = await http.get(
//   uri ,headers: <String, String>{
// 'Content-type' : 'application/json; charset=UTF-8' 
//   }
// );
// if(response.statusCode >=200 && response.statusCode<=290){
// final List<dynamic> jsonData = json.decode(response.body);
// data =jsonData.map((json)=>User.fromJson(json)).toList();
// }
//   }catch(e){
// return data;

//   }
//   return data;
// }

// }
// import 'dart:convert';
// import 'package:flutter/foundation.dart';
// import 'package:flutter_mehnah/models/User.dart';
// import 'package:http/http.dart' as http;

// class Apihandler {
//   // Use 10.0.2.2 to connect to the host's localhost from an Android emulator.
//   // For a physical device, you need to use your computer's local IP address (e.g., 192.168.1.5).
//   final String baseUri = "http://10.0.2.2:5254/api/Users";

//   Future<List<User>> getUserData() async {
//     List<User> data = [];
//     final uri = Uri.parse(baseUri);

//     try {
//       final response = await http.get(
//         uri,
//         headers: <String, String>{
//           'Content-type': 'application/json; charset=UTF-8',
//         },
//       );

//       if (response.statusCode >= 200 && response.statusCode <= 290) {
//         final List<dynamic> jsonData = json.decode(response.body);
//         data = jsonData.map((json) => User.fromJson(json)).toList();
//       } else {
//         // Print the status code for debugging if the request was not successful
//         if (kDebugMode) {
//           print('Failed to load data. Status code: ${response.statusCode}');
//         }
//       }
//     } catch (e) {
//       // Print the full error for easier debugging
//       if (kDebugMode) {
//         print('An error occurred during API call: $e');
//       }
//     }
//     return data;
//   }
// }
import 'dart:convert';
import 'package:flutter/foundation.dart';
import 'package:flutter_mehnah/models/User.dart';
import 'package:http/http.dart' as http;

class Apihandler {
  // Use 10.0.2.2 to connect to the host's localhost from an Android emulator.
  // For a physical device, you need to use your computer's local IP address (e.g., 192.168.1.5).
  final String baseUri = "http://192.168.1.102:5254/api/Users";

  Future<List<User>> getUserData() async {
    List<User> data = [];
    final uri = Uri.parse(baseUri);

    try {
      final response = await http.get(
        uri,
        headers: <String, String>{
          'Content-type': 'application/json; charset=UTF-8',
        },
      );

      if (response.statusCode >= 200 && response.statusCode <= 290) {
        final List<dynamic> jsonData = json.decode(response.body);
        data = jsonData.map((json) => User.fromJson(json)).toList();
      } else {
        // Throw an exception with a specific error message
        throw Exception('Failed to load data. Status code: ${response.statusCode}');
      }
    } catch (e) {
      // Throw an exception for any network or parsing errors
      throw Exception('An error occurred: $e');
    }
    return data;
  }
}

