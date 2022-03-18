select * 
from [dbo].[DangerCommentsContent] join [dbo].[InsultCategories]
on [dbo].[DangerCommentsContent].CommentId = [dbo].[InsultCategories].CommentId