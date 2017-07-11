SELECT
	ruin_type.name as RuinType,
	obelisk_group.name as `Group`,
	obelisk.number as Obelisk,
	obelisk.is_verified as Verified,
	ruin_site.id as GS,
	system.name as System,
	body.name as Body,
	ruin_site.latitude as Lat,
	ruin_site.longitude as `Long`
	FROM ruinsite_activeobelisks
	join obelisk on ruinsite_activeobelisks.obelisk_id = obelisk.id
	join obelisk_group on obelisk.obeliskgroup_id = obelisk_group.id
	join ruin_type on obelisk_group.ruintype_id = ruin_type.id
	join ruin_site on ruinsite_activeobelisks.ruinsite_id = ruin_site.id
	join body on ruin_site.body_id = body.id
	join system on body.system_id = system.id
	where ruin_site.id < 99997
	
	ORDER BY obelisk.obeliskgroup_id, obelisk.number, ruin_site.id