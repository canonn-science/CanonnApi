-- due to a bug in DbUp, 'delimiter' may not start at the very first character in a line...
 delimiter $$

DROP PROCEDURE IF EXISTS `up`$$

CREATE PROCEDURE up()
BEGIN

SET @version := (
	SELECT `value`
	FROM `canonndb_metadata`
	WHERE `name` = 'version'
);

SET @newVersion = 2;

IF @version < @newVersion THEN
	-- START MIGRATION SCRIPT

CREATE TABLE `artifact` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of the artifact',
	`name` VARCHAR(50) NOT NULL COMMENT 'name of the artifact',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`)
)
COMMENT='holds the actual artifacts'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `codex_category` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id for the category',
	`artifact_id` INT(11) NOT NULL COMMENT 'id of the primary artifact to unlock this ctageory',
	`name` VARCHAR(50) NOT NULL COMMENT 'name of the category',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	INDEX `FK_codexcategory_artifact` (`artifact_id`),
	CONSTRAINT `FK_codexcategory_artifact` FOREIGN KEY (`artifact_id`) REFERENCES `artifact` (`id`)
)
COMMENT='Holds the categories for the codes data'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `codex_data` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of the entry',
	`category_id` INT(11) NOT NULL COMMENT 'references the category',
	`entry_number` INT(11) NOT NULL COMMENT 'number of the entry',
	`text` TEXT NOT NULL COMMENT 'the actual entry',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	INDEX `FK_codexdata_codexcategory` (`category_id`),
	CONSTRAINT `FK_codexdata_codexcategory` FOREIGN KEY (`category_id`) REFERENCES `codex_category` (`id`)
)
COMMENT='holds the data for all the codex entries'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;


	-- END MIGRATION SCRIPT

	-- update version and add metadata	
	INSERT INTO `canonndb_metadata` (`name`, `value`) VALUES
		(CONCAT('update_v', @newVersion), DATE_FORMAT(UTC_TIMESTAMP(), '%Y-%m-%dT%TZ'));
	UPDATE `canonndb_metadata` SET `value` = @newVersion WHERE `name` = 'version';

END IF;

END$$ -- PROCUDURE up()

 delimiter ;

CALL up();

DROP PROCEDURE `up`;
