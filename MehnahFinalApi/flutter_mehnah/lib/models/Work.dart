class Work {
  final int id;
  final String description;
  final double price;
  final String? workImagesUrl;
  final String category;
  final int userId;
  final bool isAvailable;
  final String contactMethod;

  const Work({
    required this.id,
    required this.description,
    required this.price,
    this.workImagesUrl,
    required this.category,
    required this.isAvailable,
    required this.contactMethod,
    required this.userId,
  });

  // String get fullWorkImagesUrl {
  //   const ipAddress = '192.168.1.104';
  //   // Returns the full URL or an empty string if the URL is null
  //   if (workImages != null) {
  //     return 'http://$ipAddress:7232/Works/$workImages';
  //   }
  //   return '';
  // }
 String get fullWorkImagesUrl {
    const baseUrl = 'http://192.168.1.104:7232';
    return '$baseUrl$workImagesUrl';
  }
  factory Work.fromJson(Map<String, dynamic> json) {
    return Work(
      id: json['id'],
      description: json['description'],
      price: (json['price'] as num).toDouble(),
      workImagesUrl: json['workImagesUrl'],
      category: json['category'],
      userId: json['userId'] as int,
      isAvailable: json['isAvailable'] as bool,
      contactMethod: json['contactMethod'],
    );
  }
}
