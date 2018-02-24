// Function to download user info .csv file
public void DownloadCsv()
{
    var Users = from u in bc.Users
                select u;
    AdminUserViewModel vm = new AdminUserViewModel();
    List<AdminUserViewModel> viewModels = new List<AdminUserViewModel>();
    List<ApplicationUser> applicationUsers = ac.Users.ToList();
    List<User> users = Users.ToList();
    List<UserProfile> userProfiles = bc.UserProfiles.ToList();
    List<Place> places = bc.Places.ToList();
    List<Review> reviews = bc.Reviews.ToList();
    List<Post> posts = bc.Posts.ToList();
    List<Post> postFlags = bc.Posts.Where(i => i.Flag == 1).ToList();
    List<Review> reviewFlags = bc.Reviews.Where(i => i.Flag == 1).ToList();
    //pass data to list function
    vm.AdminUserList(viewModels,
     users,
     userProfiles,
     places,
     applicationUsers,
     posts,
     postFlags,
     reviews,
     reviewFlags
     );
    string usersCsv = GetCsvString(viewModels);

    // return file content with response body
    Response.ContentType = "text/csv";
    Response.AddHeader("Content-Disposition", "attachment;filename=Users.csv");
    Response.Write(usersCsv);
    Response.End();
}

// Function to create .csv file of all user email addresses
public string GetCsvString(List<AdminUserViewModel> users)
{
    StringBuilder csv = new StringBuilder();
    foreach (AdminUserViewModel user in users)
    {
        csv.Append(user.Email + ",");
    }
    csv.Length--;
    return csv.ToString();
}
