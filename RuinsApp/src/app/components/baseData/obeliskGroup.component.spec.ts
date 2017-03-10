import {async, ComponentFixture, TestBed} from '@angular/core/testing';
import {ObeliskGroupComponent} from './obeliskGroup.component';

describe('ObeliskGroupComponent', () => {
	let component: ObeliskGroupComponent;
	let fixture: ComponentFixture<ObeliskGroupComponent>;

	beforeEach(async(() => {
		TestBed.configureTestingModule({
			declarations: [ObeliskGroupComponent]
		})
			.compileComponents();
	}));

	beforeEach(() => {
		fixture = TestBed.createComponent(ObeliskGroupComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
