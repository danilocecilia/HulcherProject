﻿ALTER TABLE [dbo].[CS_Resource]  WITH CHECK ADD  CONSTRAINT [FK_CS_Resource_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_Resource] CHECK CONSTRAINT [FK_CS_Resource_CS_Job]

