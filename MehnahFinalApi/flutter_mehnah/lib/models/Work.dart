class Work{
final int  Id;
final String Description;
final double Price;
final String WorkImagesUrl;
final String Category;
final int  UserId;
final bool IsAvailable;
final String ContactMethod;

const Work({
  required this.Id,
  required this.Description,
  required this.Price,
  required this.WorkImagesUrl,
  required this.Category,
  required this.IsAvailable,
  required this.ContactMethod,
  required this.UserId,

});
 String get fullWorkImagesUrl {
    const baseUrl = 'http://192.168.1.104:7232';
    return '$baseUrl$WorkImagesUrl';
  }
static  Work fromJson(json) =>Work (
  
    Id: json['id'] ,
    Description: json['description'] ,
    Price: (json['price'] as num).toDouble(),
    WorkImagesUrl: json['workImages'] ,
    Category: json['category'] ,
    UserId: json['userId'] as int,
     ContactMethod: '',
      IsAvailable:  json['isAvailable'] as bool,
  );
}

 

