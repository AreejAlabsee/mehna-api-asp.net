import 'package:flutter/material.dart';
import 'package:flutter_mehnah/WelcomPage%20(1).dart';
import 'package:flutter_mehnah/WorkerProfile%20(1).dart';
import 'package:flutter_mehnah/mainScreen.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home:mainScreen(),
    );}}