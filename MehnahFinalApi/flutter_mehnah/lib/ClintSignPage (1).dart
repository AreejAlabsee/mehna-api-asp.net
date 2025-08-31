import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class ClintSignPage extends StatefulWidget {
  const ClintSignPage({super.key});

  @override
  State<ClintSignPage> createState() => _ClintSignPageState();
}

class _ClintSignPageState extends State<ClintSignPage> {
  final _formKey = GlobalKey<FormState>();
  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();
  final _phoneController = TextEditingController();
  String _selectedType = "Worker";

Future<void> _register() async {
  const apiUrl = "https://0.0.0.0:7219/api/Users";

  try {
    final response = await http.post(
      Uri.parse(apiUrl),
      headers: {"Content-Type": "application/json"},
      body: jsonEncode({
        "Name": _usernameController.text.trim(),
        "Password": _passwordController.text.trim(),
        "PhoneNumber": _phoneController.text.trim(),
        "UserType": _selectedType
      }),
    );

    print("Status code: ${response.statusCode}");
    print("Response body: ${response.body}");

    if (response.statusCode >= 200 && response.statusCode < 300) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("✅ User created successfully")),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text("❌ Failed: ${response.body}")),
      );
    }
  } catch (e) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text("⚠️ Error: $e")),
    );
  }
}

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        // الخلفية صورة
        decoration: const BoxDecoration(
          image: DecorationImage(
            image: AssetImage("images/b2.png"), // غيرها بمسار صورتك
            fit: BoxFit.cover,
          ),
        ),
        child: Center(
          child: SingleChildScrollView(
            padding: const EdgeInsets.all(24),
            child: Form(
              key: _formKey,
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  const Align(
                    alignment: Alignment.centerLeft,
                    child: Text(
                      "Sign In",
                      style: TextStyle(
                        fontSize: 28,
                        fontWeight: FontWeight.bold,
                        color: Colors.white,
                      ),
                    ),
                  ),
                  const SizedBox(height: 30),

                  // User Name
                  _buildTextField(_usernameController, "User Name"),
                  const SizedBox(height: 15),

                  // Password
                  _buildTextField(_passwordController, "Password", obscure: true),
                  const SizedBox(height: 15),

                  // Phone
                  _buildTextField(_phoneController, "Phone"),
                  const SizedBox(height: 15),

                  // Type (Dropdown)
                  Container(
                    padding: const EdgeInsets.symmetric(horizontal: 12),
                    decoration: BoxDecoration(
                      color: Colors.white,
                      border: Border.all(color: const Color(0xFF596442)),
                      borderRadius: BorderRadius.circular(8),
                    ),
                    child: DropdownButtonFormField<String>(
                      value: _selectedType,
                      decoration: const InputDecoration(
                        border: InputBorder.none,
                      ),
                      items: const [
                        DropdownMenuItem(
                          value: "Worker",
                          child: Text("Worker"),
                        ),
                        DropdownMenuItem(
                          value: "Client",
                          child: Text("Client"),
                        ),
                      ],
                      onChanged: (value) {
                        setState(() {
                          _selectedType = value!;
                        });
                      },
                    ),
                  ),
                  const SizedBox(height: 25),

                  // Login Button
                  ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xFF596442),
                      foregroundColor: Colors.white,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(20),
                      ),
                      padding: const EdgeInsets.symmetric(
                          horizontal: 80, vertical: 15),
                    ),
                    onPressed: () {
                      if (_formKey.currentState!.validate()) {
                        _register();
                      }
                    },
                    child: const Text("Login"),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildTextField(TextEditingController controller, String hint,
      {bool obscure = false}) {
    return TextFormField(
      controller: controller,
      obscureText: obscure,
      validator: (value) =>
          (value == null || value.isEmpty) ? "Enter $hint" : null,
      decoration: InputDecoration(
        hintText: hint,
        filled: true,
        fillColor: Colors.white,
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
          borderSide: const BorderSide(color: Color(0xFF596442)),
        ),
      ),
    );
  }
}
