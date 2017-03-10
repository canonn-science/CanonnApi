import {async, ComponentFixture, TestBed} from '@angular/core/testing';
import {RuinTypeComponent} from './ruintype.component';

describe('RuinTypeComponent', () => {
	let component: RuinTypeComponent;
	let fixture: ComponentFixture<RuinTypeComponent>;

	beforeEach(async(() => {
		TestBed.configureTestingModule({
			declarations: [RuinTypeComponent]
		})
			.compileComponents();
	}));

	beforeEach(() => {
		fixture = TestBed.createComponent(RuinTypeComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
