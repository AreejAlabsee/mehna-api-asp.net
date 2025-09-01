
// import 'package:flutter/material.dart';

// class LoginPage extends StatefulWidget {
//   @override
//   _LoginPageState createState() => _LoginPageState();
// }

// class _LoginPageState extends State<LoginPage> {
//   final _formKey = GlobalKey<FormState>();
//   final _usernameController = TextEditingController();
//   final _passwordController = TextEditingController();
//   String _selectedType = 'user'; // القيمة الافتراضية لنوع المستخدم

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       appBar: AppBar(
//         title: Text('Sign In'),
//       ),
//       body: Padding(
//         padding: const EdgeInsets.all(16.0),
//         child: Form(
//           key: _formKey,
//           child: Column(
//             mainAxisAlignment: MainAxisAlignment.center,
//             children: <Widget>[
//               TextFormField(
//                 controller: _usernameController,
//                 decoration: InputDecoration(
//                   labelText: 'User Name',
//                   border: OutlineInputBorder(),
//                 ),
//                 validator: (value) {
//                   if (value == null || value.isEmpty) {
//                     return 'Please enter your username';
//                   }
//                   return null;
//                 },
//               ),
//               SizedBox(height: 16),
//               TextFormField(
//                 controller: _passwordController,
//                 obscureText: true,
//                 decoration: InputDecoration(
//                   labelText: 'Password',
//                   border: OutlineInputBorder(),
//                 ),
//                 validator: (value) {
//                   if (value == null || value.isEmpty) {
//                     return 'Please enter your password';
//                   }
//                   return null;
//                 },
//               ),
//               SizedBox(height: 16),
//               DropdownButtonFormField<String>(
//                 value: _selectedType,
//                 decoration: InputDecoration(
//                   labelText: 'Type',
//                   border: OutlineInputBorder(),
//                 ),
//                 items: <String>['user', 'admin', 'guest']
//                     .map<DropdownMenuItem<String>>((String value) {
//                   return DropdownMenuItem<String>(
//                     value: value,
//                     child: Text(value),
//                   );
//                 }).toList(),
//                 onChanged: (String? newValue) {
//                   setState(() {
//                     _selectedType = newValue!;
//                   });
//                 },
//               ),
//               SizedBox(height: 24),
//               ElevatedButton(
//                 onPressed: () {
//                   if (_formKey.currentState!.validate()) {
//                     // تنفيذ عملية تسجيل الدخول هنا
//                     ScaffoldMessenger.of(context).showSnackBar(
//                       SnackBar(content: Text('Processing Data')),
//                     );
//                   }
//                 },
//                 child: Text('Login'),
//                 style: ElevatedButton.styleFrom(
//                   minimumSize: Size(double.infinity, 50),
//                 ),
//               ),
//             ],
//           ),
//         ),
//       ),
//     );
//   }

//   @override
//   void dispose() {
//     _usernameController.dispose();
//     _passwordController.dispose();
//     super.dispose();
//   }
// }
import 'package:flutter/material.dart';
import 'package:flutter_mehnah/ClintSignPage%20(1).dart';
import 'package:flutter_mehnah/WorkerSignPage%20(1).dart';




class WelcomePage extends StatelessWidget {
  const WelcomePage({super.key});

  @override
  Widget build(BuildContext context) {
    const Color darkOlive = Color(0xFF596442); // الأخضر الغامق من الخلفية
    const Color lightOlive = Color(0xFF8F947A);

    return Scaffold(
      body: Stack(
        children: [
          // الخلفية
          Positioned.fill(
            child: Image.asset(
              'images/FB.png',
              fit: BoxFit.cover,
            ),
          ),

          // المحتوى
          SafeArea(
            child: LayoutBuilder(
              builder: (context, constraints) {
                final h = constraints.maxHeight;

                // عرض موحد للأزرار
                const double buttonWidth = 200;
                const double buttonHeight = 45;

                return Stack(
                  children: [
                    // النص "Mehnah"
                    Positioned(
                      top: h * 0.40, // مكان النص بالنسبة لارتفاع الشاشة
                      left: 0,
                      right: 0,
                      child: const Center(
                        child: Text(
                          'Mehnah',
                          style: TextStyle(
                            fontSize: 28,
                            fontWeight: FontWeight.w600,
                            color: darkOlive,
                            letterSpacing: 0.6,
                          ),
                        ),
                      ),
                    ),

                    // الأزرار في الأسفل
                    Positioned(
                      bottom: 70,
                      left: 0,
                      right: 0,
                      child: Column(
                        children: [
            
                          // زر costumer
                          SizedBox(
                            width: buttonWidth,
                            height: buttonHeight,
                            child: OutlinedButton(
                              onPressed: ()  {  
                          Navigator.of(context).push(MaterialPageRoute(
                              builder: ((context) =>
                                  SignPage())));},
                              style: OutlinedButton.styleFrom(
                                backgroundColor: Colors.white,
                                side: BorderSide(color: darkOlive, width: 2),
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(30),
                                ),
                              ),
                              child: Text(
                                'start ',
                                style: TextStyle(
                                  fontSize: 14,
                                  color: darkOlive,
                                ),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}
