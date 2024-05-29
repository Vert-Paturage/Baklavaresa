import Table from './table.type';

type Reservation = {
    ID: number;
	NumberOfPeople: number;
	Tables: Table[];
    FirstName: string;
	LastName: string;
	Email: string;
};

export default Reservation;