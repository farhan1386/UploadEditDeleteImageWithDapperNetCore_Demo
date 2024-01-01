CREATE TABLE [dbo].[t_speaker] (
    [f_uid]             UNIQUEIDENTIFIER NOT NULL,
    [f_iid]             INT              IDENTITY (1, 1) NOT NULL,
    [f_fname]           NVARCHAR (150)   NOT NULL,
    [f_lname]           NVARCHAR (150)   NOT NULL,
    [f_gender]          INT              NULL,
    [f_speach_date]     DATETIME         NOT NULL,
    [f_speach_time]     DATETIME         NOT NULL,
    [f_speach_venue]    NVARCHAR (250)   NULL,
    [f_speaker_picture] NVARCHAR (MAX)   NULL,
    [f_create_date]     DATETIME         NULL,
    [f_update_date]     DATETIME         NULL,
    [f_delete_date]     DATETIME         NULL,
    CONSTRAINT [PK__t_speake__2244AE6B633DBFBC] PRIMARY KEY CLUSTERED ([f_uid] ASC)
);

