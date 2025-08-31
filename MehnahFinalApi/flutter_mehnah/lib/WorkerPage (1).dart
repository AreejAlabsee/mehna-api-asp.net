import 'package:flutter/material.dart';

class WorkerPage extends StatelessWidget {
  const WorkerPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        backgroundColor: const Color(0xFF6B8E23),
        leading: IconButton(
          icon: const Icon(Icons.arrow_back, color: Colors.white),
          onPressed: () {},
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
      body: Stack(
        children: [
          Positioned(
            bottom: 0,
            left: 0,
            child: ClipPath(
              clipper: _CustomClipper(),
              child: Container(
                width: MediaQuery.of(context).size.width,
                height: MediaQuery.of(context).size.height * 0.4,
                color: const Color(0xFFC0C0B4),
              ),
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              children: [
                Container(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(30.0),
                    boxShadow: [
                      BoxShadow(
                        color: Colors.grey.withOpacity(0.2),
                        spreadRadius: 1,
                        blurRadius: 5,
                        offset: const Offset(0, 3),
                      ),
                    ],
                  ),
                  child: const TextField(
                    decoration: InputDecoration(
                      hintText: 'Search',
                      border: InputBorder.none,
                      suffixIcon: Icon(Icons.search, color: Colors.grey),
                    ),
                  ),
                ),
                const SizedBox(height: 16.0),
                WorkerCard(
                  name: 'saeed hosain',
                  job: 'carpinter',
                  rating: 4.5,
                  imagePath: 'images/ph1.png', // You need to add this image to your assets
                ),
                const SizedBox(height: 16.0),
                WorkerCard(
                  name: 'fares khaled',
                  job: 'Blacksmith',
                  rating: 4.0,
                  imagePath: 'images/ph2.png', // You need to add this image to your assets
                ),
                const SizedBox(height: 16.0),
                WorkerCard(
                  name: 'hasan hamed',
                  job: 'Carpenter',
                  rating: 4.5,
                  imagePath: 'images/ph3.png', // You need to add this image to your assets
                ),
              ],
            ),
          ),
        ],
      ),
      bottomNavigationBar: BottomNavigationBar(
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Home',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: 'Profile',
          ),
        ],
        currentIndex: 0,
        selectedItemColor: const Color(0xFF6B8E23),
        unselectedItemColor: Colors.grey,
      ),
    );
  }
}

class WorkerCard extends StatelessWidget {
  final String name;
  final String job;
  final double rating;
  final String imagePath;

  const WorkerCard({super.key, 
    required this.name,
    required this.job,
    required this.rating,
    required this.imagePath,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(16.0),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(15.0),
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.2),
            spreadRadius: 1,
            blurRadius: 5,
            offset: const Offset(0, 3),
          ),
        ],
      ),
      child: Row(
        children: [
          CircleAvatar(
            radius: 30,
            backgroundImage: AssetImage(imagePath),
          ),
          const SizedBox(width: 16.0),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  '$name (${job.toLowerCase()})',
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 16,
                  ),
                ),
                const SizedBox(height: 4.0),
                Row(
                  children: List.generate(
                    5,
                    (index) {
                      return Icon(
                        index < rating.floor()
                            ? Icons.star
                            : (index < rating ? Icons.star_half : Icons.star_border),
                        color: Colors.yellow[700],
                        size: 20,
                      );
                    },
                  ),
                ),
              ],
            ),
          ),
          const Icon(Icons.play_arrow_rounded, color: Colors.grey, size: 40),
        ],
      ),
    );
  }
}

class _CustomClipper extends CustomClipper<Path> {
  @override
  Path getClip(Size size) {
    var path = Path();
    path.moveTo(0, size.height * 0.4);
    path.lineTo(0, size.height);
    path.lineTo(size.width, size.height);
    path.lineTo(size.width, size.height * 0.4);
    path.quadraticBezierTo(size.width * 0.5, size.height * 0.7, 0, size.height * 0.4);
    return path;
  }

  @override
  bool shouldReclip(covariant CustomClipper<Path> oldClipper) {
    return false;
  }
}