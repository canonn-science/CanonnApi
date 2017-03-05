/*
 ruins db version 2
 creates tables:
 - ruin_type
 - ruin_layout
 - obelisk_group
 - obelisk
 
 inserts initial data
 - ruin_type
 
*/

CREATE TABLE `ruin_type` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`name` TEXT NOT NULL,
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`)
)
COMMENT='Holds information about the types of ruins found'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;


ALTER TABLE `ruin_type` CONVERT TO CHARACTER SET utf8;

INSERT INTO `ruin_type` (`id`, `name`) VALUES
	(1, 'Alpha'),
	(2, 'Beta'),
	(3, 'Gamma');
