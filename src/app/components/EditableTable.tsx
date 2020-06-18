import { useState } from "react";
import MaterialTable, { Column, Options } from 'material-table'
import { BusinessContactRecord } from "../interfaces";
import fetch from '../utils/fetch';

type PagedResult<T> = {
    data: T,
    page: number,
    totalCount: number
}

const fetchData = (query) => {
    return new Promise<PagedResult<BusinessContactRecord[]>>((resolve, reject) => {
        let url = process.env.NEXT_PUBLIC_API_BASE + '/api/users/1/records/paged?'
        url += 'pageSize=' + query.pageSize
        url += '&page=' + (query.page + 1)
        fetch<PagedResult<BusinessContactRecord[]>>(url)
            .then(result => {
                result.page--;
                resolve(result)
            })
    })
}

export default function EditableTable() {
    //const { useState } = React;

    const [columns, setColumns] = useState<Column<BusinessContactRecord>[]>([
        { title: 'Id', field: 'id', editable: 'never', hidden: true },
        { title: 'User Id', field: 'userId', editable: 'never', hidden: true },
        { title: 'Name', field: 'name' },
        { title: 'E-mail', field: 'email' },
        { title: 'Address Line1', field: 'address.line1' },
        { title: 'Address Line2', field: 'address.line2' },
        { title: 'Address City', field: 'address.city' },
        { title: 'Address State/Province', field: 'address.stateOrProvince' },
        { title: 'Address Postal Code', field: 'address.postalCode' },
    ]);

    const tableOptions: Options = {
        pageSizeOptions: [50, 100, 250],
        pageSize: 100
    }

    return (
        <MaterialTable
            title="Editable Preview"
            columns={columns}
            data={fetchData}
            options={tableOptions}
            editable={{
                onRowAdd: newData =>
                    new Promise((resolve, reject) => {
                        setTimeout(() => {
                            // API call to insert new data
                            resolve();
                        }, 1000)
                    }),
                onRowUpdate: (newData, oldData) =>
                    new Promise((resolve, reject) => {
                        setTimeout(() => {
                            // API call to update new data
                            resolve();
                        }, 1000)
                    }),
                onRowDelete: oldData =>
                    new Promise((resolve, reject) => {
                        setTimeout(() => {
                            // API call to remove data
                            resolve()
                        }, 1000)
                    }),
            }}
        />
    )
}
