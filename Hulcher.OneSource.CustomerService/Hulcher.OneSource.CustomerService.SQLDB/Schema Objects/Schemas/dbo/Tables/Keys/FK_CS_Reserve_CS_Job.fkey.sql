﻿ALTER TABLE [dbo].[CS_Reserve]  WITH CHECK ADD  CONSTRAINT [FK_CS_Reserve_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_Reserve] CHECK CONSTRAINT [FK_CS_Reserve_CS_Job]

