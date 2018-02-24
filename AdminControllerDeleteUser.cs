
// POST: Admin/Delete/5
[HttpPost, ActionName("DeleteUser")]
[ValidateAntiForgeryToken]
public ActionResult DeleteUserConfirmed(string id)
{
    // open the data connection
    using (SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\User\\Source\\Workspaces\\Workspace\\Bewander\\Bewander\\App_Data\\Bewander.mdf; Integrated Security = True"))
    {
        // disable the foreign key constraints preventing user deletion
        SqlCommand disableConstraint1 = new SqlCommand("ALTER TABLE [Like] NOCHECK CONSTRAINT [FK_dbo.Like_dbo.Comment_Comment_ID]", con);
        con.Open();
        disableConstraint1.ExecuteNonQuery();

        SqlCommand disableConstraint2 = new SqlCommand("ALTER TABLE [Like] NOCHECK CONSTRAINT [FK_dbo.Like_dbo.Post_Post_ID]", con);
        disableConstraint2.ExecuteNonQuery();

        // delete all records connected to the user
        SqlCommand cmd1 = new SqlCommand("DELETE FROM [Like] WHERE UserID = @id", con);
        cmd1.Parameters.AddWithValue("@id", id);
        cmd1.ExecuteNonQuery();

        SqlCommand cmd2 = new SqlCommand("DELETE FROM dbo.Comment WHERE User_UserID = @id", con);
        cmd2.Parameters.AddWithValue("@id", id);
        cmd2.ExecuteNonQuery();

        SqlCommand cmd3 = new SqlCommand("DELETE FROM dbo.Post WHERE UserID = @id", con);
        cmd3.Parameters.AddWithValue("@id", id);
        cmd3.ExecuteNonQuery();

        SqlCommand cmd4 = new SqlCommand("DELETE FROM [File] WHERE UserID = @id", con);
        cmd4.Parameters.AddWithValue("@id", id);
        cmd4.ExecuteNonQuery();

        SqlCommand cmd5 = new SqlCommand("DELETE FROM [Review] WHERE UserID = @id", con);
        cmd5.Parameters.AddWithValue("@id", id);
        cmd5.ExecuteNonQuery();

        // delete the user
        SqlCommand cmd6 = new SqlCommand("DELETE FROM [User] WHERE UserID = @id", con);
        cmd6.Parameters.AddWithValue("@id", id);
        cmd6.ExecuteNonQuery();

        // reenable the foreign key constraints preventing user deletion
        SqlCommand reenableConstraint1 = new SqlCommand("ALTER TABLE [Like] WITH CHECK CHECK CONSTRAINT [FK_dbo.Like_dbo.Comment_Comment_ID]", con);
        disableConstraint1.ExecuteNonQuery();

        SqlCommand reenableConstraint2 = new SqlCommand("ALTER TABLE [Like] WITH CHECK CHECK CONSTRAINT [FK_dbo.Like_dbo.Post_Post_ID]", con);
        disableConstraint2.ExecuteNonQuery();
    }
    bc.SaveChanges();
    return RedirectToAction("Index");
}