// Generate partial view to display the 8 most-liked reviews on the Home Index
public PartialViewResult _HomeImages()
{
    List<ReviewViewModel> reviewImages = new List<ReviewViewModel>();
    IEnumerable<Review> reviews = db.Reviews.ToList();

    foreach (Review review in reviews)
    {
        Image picture = (review.ImageID != null) ? db.Images.Find(review.ImageID) : new Image();

        if (review.ImageID != null && picture != null)
        {
            Place place = db.Places.Find(review.PlaceID);
            bool imageLikeToView = PublicImageLikedState(review.ImageID);
            int totalLikes = db.ImageLike.Count(i => i.FileID == review.ImageID);
            ReviewViewModel newReview = new ReviewViewModel(review, picture, imageLikeToView, totalLikes, place);
            reviewImages.Add(newReview);
        }
    }
    return PartialView("_HomeImages", reviewImages.OrderByDescending(i => i.ImageLikeCount).Take(8));
}