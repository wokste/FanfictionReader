CREATE TABLE Story (
    Pk                INTEGER  PRIMARY KEY AUTOINCREMENT
                               NOT NULL,
    Host              STRING   NOT NULL,
    Id                INTEGER  NOT NULL
                               UNIQUE,
    AuthorId          INTEGER  DEFAULT (0) 
                               NOT NULL,
    Title             STRING   DEFAULT ""
                               NOT NULL,
    AddDate           DATETIME NOT NULL
                               DEFAULT NULL,
    ChapterCount      INTEGER  DEFAULT (0) 
                               NOT NULL,
    LastReadChapterId INTEGER  NOT NULL
                               DEFAULT (0),
    LastReadDate      DATETIME DEFAULT NULL
                               NOT NULL,
    IsComplete        BOOLEAN  DEFAULT (0) 
                               NOT NULL,
    MinimumAge        INTEGER  DEFAULT (0) 
                               NOT NULL,
    Words             INTEGER  DEFAULT (0) 
                               NOT NULL,
    PublishDate       DATETIME DEFAULT NULL
                               NOT NULL,
    UpdateDate        DATETIME NOT NULL
                               DEFAULT NULL,
    MetaCheckDate     DATETIME NOT NULL
                               DEFAULT NULL
);
