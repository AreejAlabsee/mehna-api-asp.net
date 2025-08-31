class User{
  final int id;
  final String Name;
  final String UserType;
 final String PhoneNumber;
 final String Password;
 final String ProfileImage;

const User({
  required  this.id,
  required  this.Name,
  required  this.UserType,
   required this.PhoneNumber,
   required this.Password,
   required this.ProfileImage,
  });
 
// A getter to return the full image URL
  String get fullProfileImageUrl {
    const baseUrl = 'http://192.168.1.104:7232';
    return '$baseUrl$ProfileImage';
  }
static User fromJson(json) => User(
 id: json['id'] ,
      Name: json['name'],
      UserType: json['userType'] ,
      PhoneNumber: json['phoneNumber'] ,
      Password: json['password'] ,
      ProfileImage: json['profileImage'] ,
    

);}