import 'package:flutter/material.dart';

class WorkerSignInPage extends StatefulWidget {
  const WorkerSignInPage({super.key});

  @override
  State<WorkerSignInPage> createState() => _WorkerSignInPageState();
}

class _WorkerSignInPageState extends State<WorkerSignInPage> {
  final _formKey = GlobalKey<FormState>();
  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();
  String _selectedType = 'User';

  @override
  void dispose() {
    _usernameController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          // الخلفية
          Container(
            decoration: const BoxDecoration(
              image: DecorationImage(
                image: AssetImage("images/b2.png"),
                fit: BoxFit.cover,
              ),
            ),
          ),

          SafeArea(
            child: Column(
              children: [
                // نص Sign In في الزاوية العلوية اليسرى
                Align(
                  alignment: Alignment.topLeft,
                  child: Padding(
                    padding: const EdgeInsets.only(left: 20, top: 20),
                    child: const Text(
                      "Sign In",
                      style: TextStyle(
                        fontSize: 28,
                        fontWeight: FontWeight.bold,
                        color: Colors.white,
                      ),
                    ),
                  ),
                ),

                // باقي محتوى الصفحة
                Center(
                  child: Expanded(
                    child: SingleChildScrollView(
                      child: Form(
                        key: _formKey,
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            const SizedBox(height: 50),
                
                            // حقل اسم المستخدم
                            _buildFormField(
                              controller: _usernameController,
                              hintText: "User Name",
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return 'Please enter your username';
                                }
                                return null;
                              },
                            ),
                            const SizedBox(height: 15),
                
                            // حقل كلمة المرور
                            _buildFormField(
                              controller: _passwordController,
                              hintText: "Password",
                              obscureText: true,
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return 'Please enter your password';
                                }
                                if (value.length < 6) {
                                  return 'Password must be at least 6 characters';
                                }
                                return null;
                              },
                            ),
                            const SizedBox(height: 15),
                
                            // حقل نوع المستخدم
                            Padding(
                              padding: const EdgeInsets.symmetric(horizontal: 30),
                              child: DropdownButtonFormField<String>(
                                initialValue: _selectedType,
                                decoration: InputDecoration(
                                  hintText: "Type",
                                  contentPadding: const EdgeInsets.symmetric(
                                      vertical: 12, horizontal: 15),
                                  filled: true,
                                  fillColor: Colors.white,
                                  border: OutlineInputBorder(
                                    borderRadius: BorderRadius.circular(8),
                                  ),
                                ),
                                items: <String>['User', 'Admin', 'Guest']
                                    .map<DropdownMenuItem<String>>((String value) {
                                  return DropdownMenuItem<String>(
                                    value: value,
                                    child: Text(value),
                                  );
                                }).toList(),
                                onChanged: (String? newValue) {
                                  setState(() {
                                    _selectedType = newValue!;
                                  });
                                },
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    return 'Please select user type';
                                  }
                                  return null;
                                },
                              ),
                            ),
                            const SizedBox(height: 40),
                
                            // زر تسجيل الدخول
                            ElevatedButton(
                              style: ElevatedButton.styleFrom(
                                backgroundColor: const  Color(0xFF596442),
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(20),
                                ),
                                padding: const EdgeInsets.symmetric(
                                    horizontal: 80, vertical: 15),
                              ),
                              onPressed: () {
                                if (_formKey.currentState!.validate()) {
                                  ScaffoldMessenger.of(context).showSnackBar(
                                    const SnackBar(
                                      content: Text('Logging in...'),
                                      duration: Duration(seconds: 2),
                                    ),
                                  );
                                }
                              },
                              child: const Text(
                                "Login",
                                style: TextStyle(fontSize: 16, color: Colors.white),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildFormField({
    required TextEditingController controller,
    required String hintText,
    bool obscureText = false,
    required String? Function(String?) validator,
  }) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 30),
      child: TextFormField(
        controller: controller,
        obscureText: obscureText,
        decoration: InputDecoration(
          hintText: hintText,
          contentPadding:
              const EdgeInsets.symmetric(vertical: 12, horizontal: 15),
          filled: true,
          fillColor: Colors.white,
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(8),
          ),
        ),
        validator: validator,
      ),
    );
  }
}