SELECT
	`obelisk`.`id` AS `#`,
	`ruin_type`.`name` AS `Type`,
	`obelisk_group`.`name` AS `Group`,
	`obelisk`.`number` AS `Number`,
	`ruin_site`.`id` AS `GS#`,
	`system`.`name` AS `System`,
	`body`.`name` AS `Body`,
	(SELECT COUNT(*) FROM `obelisk`
		INNER JOIN `ruinsite_activeobelisks` ON `ruinsite_activeobelisks`.`obelisk_id` = `obelisk`.`id`
		WHERE `obelisk`.`codexdata_id` IS NULL
		AND `ruinsite_activeobelisks`.`ruinsite_id` = `ruin_site`.`id`) AS `# on this site`
	FROM `obelisk`
	JOIN `obelisk_group` ON `obelisk_group`.`id` = `obelisk`.`obeliskgroup_id`
	JOIN `ruin_type` ON `ruin_type`.`id` = `obelisk_group`.`ruintype_id`
	INNER JOIN `ruinsite_activeobelisks` ON `ruinsite_activeobelisks`.`obelisk_id` = `obelisk`.`id`
	INNER JOIN `ruin_site` ON `ruin_site`.`id` = `ruinsite_activeobelisks`.`ruinsite_id`
	INNER JOIN `location` ON `ruin_site`.`location_id` = `location`.`id`
	JOIN `body` ON `body`.`id` = `location`.`body_id`
	JOIN `system` ON `system`.`id` = `location`.`system_id`
	WHERE `obelisk`.`codexdata_id` IS NULL
		AND `ruinsite_activeobelisks`.`ruinsite_id` < 9997
	ORDER BY `# on this site` DESC, `System`, `Body`, `Type`, `Group`, `Number`;
