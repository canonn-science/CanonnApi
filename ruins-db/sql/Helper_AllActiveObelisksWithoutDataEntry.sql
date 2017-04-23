SELECT
	`obelisk`.`id` AS `#`, 
	`ruin_type`.`name` AS `Type`,
	`obelisk_group`.`name` AS `Group`,
	`obelisk`.`number` AS `Number`,
	`ruin_site`.`id` AS `GS#`,
	`system`.`name` AS `System`,
	`body`.`name` AS `Body`
	FROM `obelisk`
	JOIN `obelisk_group` ON `obelisk_group`.`id` = `obelisk`.`obeliskgroup_id`
	JOIN `ruin_type` ON `ruin_type`.`id` = `obelisk_group`.`ruintype_id`
	INNER JOIN `ruinsite_activeobelisks` ON `ruinsite_activeobelisks`.`obelisk_id` = `obelisk`.`id`
	INNER JOIN `ruin_site` ON `ruin_site`.`id` = `ruinsite_activeobelisks`.`ruinsite_id`
	JOIN `body` ON `body`.`id` = `ruin_site`.`body_id`
	JOIN `system` ON `system`.`id` = `body`.`system_id`
	WHERE `obelisk`.`codexdata_id` IS NULL
		AND `ruinsite_activeobelisks`.`ruinsite_id` < 9997
	ORDER BY `Type`, `Group`, `Number`;
	