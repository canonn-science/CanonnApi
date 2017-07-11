UPDATE `obelisk` SET `is_verified` = 0;

UPDATE
	`obelisk`
	JOIN `obelisk_group` ON `obelisk`.`obeliskgroup_id` = `obelisk_group`.`id` 
	JOIN `ruin_type` ON `obelisk_group`.`ruintype_id` = `ruin_type`.`id`
SET `is_verified` = 1
WHERE
	( CONCAT(`obelisk_group`.`name`, LPAD(`obelisk`.`number`, 2, '0')) IN (
		'A06', 'A14', 'B06', 'B12', 'B18', 'B20', 'C01', 'C03', 'C06', 'C08',
		'C13', 'D01', 'F08', 'F12', 'F13', 'F19', 'F21', 'G02', 'G03', 'H03',
		'I01', 'I06', 'I07', 'I09', 'I10', 'I12', 'I16', 'J01', 'J04', 'J05',
		'L04', 'L06', 'L07', 'M01', 'M03', 'M05', 'M07', 'M10', 'M12', 'M14',
		'M17', 'M19', 'M20', 'M24', 'N01', 'N05', 'O07', 'O12', 'O15', 'O16',
		'O19', 'O20', 'P05', 'P07', 'P09', 'P10'
	)
	AND `ruin_type`.`name` = 'Alpha')
	OR
	( CONCAT(`obelisk_group`.`name`, LPAD(`obelisk`.`number`, 2, '0')) IN (
		'A09', 'B03', 'C09', 'C10', 'C11', 'C15', 'C17', 'C20', 'C22', 'D06',
		'E15', 'E22', 'G04', 'G13', 'G16', 'G18', 'H05', 'H09', 'I05', 'I08',
		'I12', 'J03', 'J09', 'K04', 'K05', 'K21', 'K24', 'K25', 'K26', 'L06',
		'M02', 'N02', 'N08', 'N24', 'O02', 'O03', 'P02', 'P23', 'Q01', 'Q04',
		'R12', 'R14', 'R18', 'R23', 'T13', 'T14', 'T16', 'T18', 'U02', 'U04',
		'U12', 'U15', 'U21', 'U36', 'U41', 'U46'
	)
	AND `ruin_type`.`name` = 'Beta')
	OR
	( CONCAT(`obelisk_group`.`name`, LPAD(`obelisk`.`number`, 2, '0')) IN (
		'A04', 'B04', 'B12', 'B24', 'B27', 'C02', 'C03', 'C07', 'C09', 'C13',
		'C14', 'C15', 'D01', 'D12', 'D14', 'E04', 'E05', 'E07', 'F03', 'F06',
		'F08', 'F09', 'F10', 'G03', 'G05', 'G13', 'H04', 'H22', 'H28', 'H30',
		'H45', 'H52', 'I01', 'I02', 'I05', 'I07', 'I09', 'I16', 'J04', 'K03',
		'K05', 'K12', 'L02', 'L05', 'L09', 'M01', 'M10', 'M12', 'N01', 'N12', 
		'O02', 'O05', 'P26', 'P27', 'Q07', 'R13', 'S05'
	)
	AND `ruin_type`.`name` = 'Gamma')
	