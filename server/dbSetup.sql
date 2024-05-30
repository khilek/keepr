-- Active: 1715613927110@@127.0.0.1@3306@patient_sorceress_6d50d1_db
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture',
    coverImg varchar(1000) NOT NULL
) default charset utf8mb4 COMMENT '';

ALTER TABLE accounts ADD COLUMN coverImg TEXT;

ALTER TABLE accounts DROP COLUMN coverImg;

DROP TABLE accounts;

DROP TABLE cars;

CREATE TABLE IF NOT EXISTS keep (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    description VARCHAR(10000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    views INT NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE keep

CREATE TABLE IF NOT EXISTS vault (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    isPrivate BOOLEAN NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE keep

CREATE TABLE IF NOT EXISTS vaultkeep (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    creatorId VARCHAR(255) NOT NULL,
    keepId INT NOT NULL,
    vaultId INT NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (keepId) REFERENCES keep (id) ON DELETE CASCADE,
    FOREIGN KEY (vaultId) REFERENCES vault (id) ON DELETE CASCADE
)

INSERT INTO
    vaultkeep (keepId, vaultId, creatorId)
VALUES (
        22,
        54,
        "6616fce39bbe0efdcc07f907"
    );

SELECT vaultkeep.*, keep.*, accounts.*
FROM
    vaultkeep
    JOIN keep ON vaultkeep.keepId = keep.id
    JOIN accounts on accounts.id = vaultkeep.creatorId
WHERE
    vaultkeep.vaultId = 51;

SELECT vaultkeep.*, COUNT(vaultkeep.id) AS kept, accounts.*
FROM
    vaultkeep
    JOIN accounts ON vaultkeep.creatorId = accounts.id
    LEFT JOIN keep ON vaultkeep.keepId = vaultkeep.id
GROUP BY (vaultkeep.id);

-- SELECT
-- keep.*,
-- COUNT(vaultkeep.id) AS kept,
-- accounts.*
-- FROM
--     keep
--     JOIN accounts ON keep.creatorId = accounts.id
--     LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id
-- GROUP BY (keep.id)

-- SELECT keep.*, COUNT(vaultkeep.id) AS kept, accounts.*
-- FROM
--     keep
--     JOIN accounts ON keep.creatorId = accounts.id
--     LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id
-- GROUP BY (keep.id)

SELECT keep.*, COUNT(vaultkeep.id) AS kept, accounts.*
FROM
    keep
    JOIN accounts ON keep.creatorId = accounts.id
    LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id
GROUP BY (keep.id);

SELECT keep.*, COUNT(vaultkeep.id) AS kept, accounts.*
FROM
    keep
    JOIN accounts ON accounts.id = keep.creatorId
    LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id
GROUP BY (keep.id);

WHERE keep.id = @keepId;

";


            string sql = @"
SELECT keep.*, accounts.*
FROM keep
    JOIN accounts ON accounts.id = keep.creatorId
WHERE
    keep.id = @keepId;