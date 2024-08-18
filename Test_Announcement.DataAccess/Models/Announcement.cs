using System;
using System.Collections.Generic;

namespace Test_Announcement.DataAccess.Models;

public partial class Announcement
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateOfPublishing { get; set; }

    public DateTime EventDate { get; set; }
}
