SELECT DISTINCT
	`obelisk`.`id` AS `#`, 
	`obelisk`.`is_broken` AS `Broken`, 
	`ruin_type`.`name` AS `Type`,
	`obelisk_group`.`name` AS `Group`,
	`obelisk`.`number` AS `Number`
	FROM `obelisk`
	JOIN `obelisk_group` ON `obelisk_group`.`id` = `obelisk`.`obeliskgroup_id`
	JOIN `ruin_type` ON `ruin_type`.`id` = `obelisk_group`.`ruintype_id`
	WHERE `obelisk`.`id` NOT IN (SELECT DISTINCT `obelisk_id` FROM `ruinsite_activeobelisks` WHERE `ruinsite_activeobelisks`.`ruinsite_id` < 9997)
		AND `obelisk`.`is_broken` = false
	ORDER BY `Type`, `Group`, `Number`;
