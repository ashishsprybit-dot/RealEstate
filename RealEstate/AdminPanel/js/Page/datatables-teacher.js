"use strict";
function _classCallCheck(instance, Constructor) {
    if (!(instance instanceof Constructor)) {
        throw new TypeError("Cannot call a class as a function");
    }
}

function _defineProperties(target, props) {
    for (var i = 0; i < props.length; i++) {
        var descriptor = props[i];
        descriptor.enumerable = descriptor.enumerable || false;
        descriptor.configurable = true;
        if ("value" in descriptor) descriptor.writable = true;
        Object.defineProperty(target, descriptor.key, descriptor);
    }
}

function _createClass(Constructor, protoProps, staticProps) {
    if (protoProps) _defineProperties(Constructor.prototype, protoProps);
    if (staticProps) _defineProperties(Constructor, staticProps);
    return Constructor;
}

// DataTables Demo
// =============================================================
var DataTablesDemo = /*#__PURE__*/ function () {
    function DataTablesDemo() {
        _classCallCheck(this, DataTablesDemo);

        this.init();
    }

    function getFullName(data, type, dataToSet) {
        return data.FirstName + ' ' + data.LastName;
    }

    function getStatus(data, type, dataToSet) {
        if (data.Status == true)
            return 'Active';
        else
            return 'Inactive';
    }

    _createClass(DataTablesDemo, [{
        key: "init",
        value: function init() {
            // event handlers
            this.table = this.table();
            this.searchRecords();
            this.selecter();
            this.clearSelected();
            //this.table2(); // add buttons
            //this.table.buttons().container().appendTo('#dt-buttons').unwrap();
        }
    },
    {
        key: "table",
        value: function table() {
            var initDataTable = $('#myTable').DataTable({
                dom: "<'text-muted'Bi>\n        <'table-responsive'tr>\n        <'mt-4'p>",
                buttons: [],
                //'copyHtml5', {
                //    extend: 'print',
                //    autoPrint: false
                //}],
                language: {
                    paginate: {
                        previous: '<i class="fa fa-lg fa-angle-left"></i>',
                        next: '<i class="fa fa-lg fa-angle-right"></i>'
                    }
                },
                data: userdata,
                autoWidth: false,
                deferRender: true,
                pageLength: 25,
                order: [1, 'asc'],
                columns: [
                    //{
                    //    data: 'ID',
                    //    className: 'col-checker align-middle',
                    //    orderable: false,
                    //    searchable: false
                    //},
                    {
                        data: getFullName,
                        className: 'align-middle'
                    },
                    {
                        data: 'EmailID',
                        className: 'align-middle'
                    },
                    {
                        data: 'Phone',
                        className: 'align-middle'
                    },
                    {
                        data: "Status",
                        className: 'align-middle text-center',
                        orderable: false
                    },
                    {
                        data: "Edit",
                        className: 'align-middle text-center',
                        orderable: false
                    },
                    {
                        data: "Delete",
                        className: 'align-middle text-center',
                        orderable: false,
                        searchable: false
                    }
                ],
                columnDefs: [                   
                    {
                        targets: 0,
                        render: function render(data, type, row, meta) {
                            return "<a href=\"user-management-modify.aspx?id=" + row.ID + "\" class=\"mr-1\">" + getFullName(row) + "</a>";                            
                        }
                    },
                    {                        
                        targets: 3,
                        render: function render(data, type, row, meta) {
                            return "<a href='javascript:;' id='lnk" + row.ID  + "' onclick='GridOperation(" + divMsg + ",\"" + (row.Status == true ? "inactive" : "active") + "\"," + row.ID + ");' ><span class='list-icon'><span class='" + (row.Status == true ? 'oi oi-circle-check text-success' : 'oi oi-circle-x text-red') + "'></span></span></a>";
                        }
                    },
                    {
                        targets: 4,
                        render: function render(data, type, row, meta) {                            
                            return "<a class='btn btn-sm btn-icon btn-secondary' href='user-management-modify.aspx?id=" + row.ID + "'><i class=\"fa fa-pencil-alt\"></i></a>";                                
                        }
                    },
                    {
                        targets: 5,
                        render: function render(data, type, row, meta) {
                            return "<a class='btn btn-sm btn-icon btn-secondary' href='javascript:void(0)'  onclick='GridOperation(" + divMsg + ",\"remove\"," + row.ID + ");' ><i class=\"far fa-trash-alt\"></i></a><a class='datatable-delete lnkdelete" + row.ID + "' href='javascript:;' style='display: none;'>Delete</a>";
                        }
                    }
                ]
            });
            $('#myTable a.datatable-delete').on('click', function (e) {
                e.preventDefault();
                var nRow = $(this).parents('tr')[0];                
                initDataTable.row(nRow).remove().draw();
            });
            return initDataTable;
        }
    },
    {
        key: "searchRecords",
        value: function searchRecords() {
            var self = this;
            $('#table-search, #filterBy').on('keyup change focus', function (e) {
                var filterBy = $('#filterBy').val();
                var hasFilter = filterBy !== '';
                var value = $('#table-search').val(); // clear selected rows

                if (value.length && (e.type === 'keyup' || e.type === 'change')) {
                    self.clearSelectedRows();
                } // reset search term


                self.table.search('').columns().search('').draw();

                if (hasFilter) {
                    self.table.columns(filterBy).search(value).draw();
                } else {
                    self.table.search(value).draw();
                }
            });
        }
    }, {
        key: "getSelectedInfo",
        value: function getSelectedInfo() {
            var $selectedRow = $('input[name="selectedRow[]"]:checked').length;
            var $info = $('.thead-btn');
            var $badge = $('<span/>').addClass('selected-row-info text-muted pl-1').text("".concat($selectedRow, " selected")); // remove existing info

            $('.selected-row-info').remove(); // add current info

            if ($selectedRow) {
                $info.prepend($badge);
            }
        }
    }, {
        key: "selecter",
        value: function selecter() {
            var self = this;
            $(document).on('change', '#check-handle', function () {
                var isChecked = $(this).prop('checked');
                $('input[name="selectedRow[]"]').prop('checked', isChecked); // get info

                self.getSelectedInfo();
            }).on('change', 'input[name="selectedRow[]"]', function () {
                var $selectors = $('input[name="selectedRow[]"]');
                var $selectedRow = $('input[name="selectedRow[]"]:checked').length;
                var prop = $selectedRow === $selectors.length ? 'checked' : 'indeterminate'; // reset props

                $('#check-handle').prop('indeterminate', false).prop('checked', false);

                if ($selectedRow) {
                    $('#check-handle').prop(prop, true);
                } // get info


                self.getSelectedInfo();
            });
        }
    }, {
        key: "clearSelected",
        value: function clearSelected() {
            var self = this; // clear selected rows

            $('#myTable').on('page.dt', function () {
                self.clearSelectedRows();
            });
            $('#clear-search').on('click', function () {
                self.clearSelectedRows();
            });
        }
    }, {
        key: "clearSelectedRows",
        value: function clearSelectedRows() {
            $('#check-handle').prop('indeterminate', false).prop('checked', false).trigger('change');
        }
    }
    ]);

    return DataTablesDemo;
}();

$(document).on('ready', function () {
    new DataTablesDemo();
});